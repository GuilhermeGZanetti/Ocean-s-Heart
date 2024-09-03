using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class BattleBoat: MonoBehaviour
{
    [SerializeField] private BattleSceneManager battleSceneManager;
    public ScriptableBoatStats scriptableBoatStats;
    public BoatStats boatStats;

    public GameObject cannonPrefab;
    

    public float sailStatus = 0.0f; // 0.0 to 1.0
    public float rudderPosition = 0.0f; // -1.0 to 1.0

    public float baseForce = 0.3f;
    public float rotationSpeed = 2.0f; // Speed at which the boat rotates towards its target direction
    public float rudderEffect = 6f;

    private float leftCannonCooldown = 0.0f;
    private float rightCannonCooldown = 0.0f;



    private Rigidbody2D rb;

    void Start(){
        boatStats = scriptableBoatStats.baseStats;
        //Based on the base speed, weight and max weight, calculate speed
        boatStats.speed = scriptableBoatStats.baseStats.speed + (boatStats.maxWeight - boatStats.weight)/(2*boatStats.maxWeight);

        rb = GetComponent<Rigidbody2D>();

        //Get battle scene manager
        battleSceneManager = GameObject.Find("SceneManager").GetComponent<BattleSceneManager>();
    }

    void Update(){
        if (leftCannonCooldown > 0.0f){
            leftCannonCooldown -= Time.deltaTime;
        }
        if (rightCannonCooldown > 0.0f){
            rightCannonCooldown -= Time.deltaTime;
        }
    }

    void FixedUpdate(){
        // Apply force depending on sail status and rudder position
        float magnitude = boatStats.speed * sailStatus * baseForce;
        float angle = (float)(rudderPosition * (rudderEffect * Math.PI / 180));
        Vector2 force = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * magnitude;
        rb.AddRelativeForce(force);

        // Rotate the player's ship to face the direction of movement using rigidbody
        RotateToMovementDirection();
    }

    void RotateToMovementDirection()
    {
        // Get the current velocity vector of the Rigidbody2D
        Vector2 velocity = rb.velocity;

        // If the boat is moving, calculate the rotation angle and apply it
        if (velocity.magnitude > 0.01f) // Avoid rotation when velocity is near zero
        {
            float targetAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90f; // Convert to degrees and adjust angle
            float currentAngle = rb.rotation;

            // Gradually rotate towards the target angle using Mathf.LerpAngle for smooth rotation
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);

            rb.rotation = newAngle; // Set the Rigidbody2D's rotation
        }
    }

    public void SetTargetRudderPosition(float target_pos, float deltaTime){
        target_pos = Mathf.Clamp(target_pos, -1.0f, 1.0f);  
        // Lerp rudder position towards target position
        rudderPosition = Mathf.Lerp(rudderPosition, target_pos, deltaTime*boatStats.manuverability);
    }

    public void ChangeSailStatus(float delta_pos){
        sailStatus += delta_pos;
        sailStatus = Mathf.Clamp(sailStatus, 0.0f, 1.0f);
    }

    public void TakeDamage(float damage){
        boatStats.hp -= damage;
        if (boatStats.hp <= 0.0f){
            Debug.Log("Boat Destroyed");
            battleSceneManager.BoatDestroyed(gameObject);
        }
    }

    public void ShootCanons(Vector2 direction){
        bool canShoot = false;
        if (direction.x > 0 && leftCannonCooldown <= 0.0f){
            leftCannonCooldown = scriptableBoatStats.baseStats.cooldown;
            canShoot = true;
        } else if (direction.x < 0 && rightCannonCooldown <= 0.0f){
            rightCannonCooldown = scriptableBoatStats.baseStats.cooldown;
            canShoot = true;
        }
        if(!canShoot){
            return;
        }
        for (int i = 0; i < boatStats.num_canons; i++){
            // Calculate the angle between the boat's forward direction (up in local space) and the direction vector
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // Create a rotation based on this angle
            Quaternion ballRotation = Quaternion.Euler(0, 0, angle) * transform.rotation;
            
            // Calculate cannonball position
            float x = 0f;
            if (boatStats.num_canons == 1){
                x = 0f;
            } else {
                x = (-2/(1-boatStats.num_canons)*i - 1)*0.5f;
            }
            Vector3 cannonPos = new Vector3(0, x, 0);
            // Transform cannonPos to world space
            cannonPos = transform.TransformPoint(cannonPos);

            // Instantiate the cannonball with the calculated rotation
            GameObject cannon = Instantiate(cannonPrefab, cannonPos, ballRotation);
            cannon.transform.parent = gameObject.transform;
            cannon.GetComponent<CannonBall>().Init(gameObject.name);
        }
    }

    public float GetLootedGold(){
        return scriptableBoatStats.lootable_gold;
    }
}
