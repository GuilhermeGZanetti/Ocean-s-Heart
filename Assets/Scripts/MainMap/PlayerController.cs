using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    NavMeshAgent agent;
    public Boat boat;
    public float agentSpeedMultiplier = 0.1f;
    [SerializeField] private MapSceneManager mapSceneManager;

    private GameObject targetGameObject = null;
    private bool isSafeInCity = false;
    private float totalGoldRun = 0f;


    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = boat.boatStats.speed;
    }

    // Update is called once per frame
    void Update(){
        agent.speed = boat.boatStats.speed * agentSpeedMultiplier;
        if (Input.GetMouseButtonDown(0)){
            isSafeInCity = false;
            targetGameObject = null;
            //Check if clicked on a tilemap
            RaycastHit2D hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Input.mousePosition), 100f);
            if (hit.transform == null){
                // Get the mouse position in world space
                Vector3 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                worldPoint.z = 0;
                // Debug.Log("World Point: " + worldPoint);
                // Debug.Log("Agent Position: " + transform.position);
                agent.SetDestination(worldPoint);
            } else if (hit.transform.gameObject.GetComponent<PirateController>()){
                GoToGameObject(hit.transform.gameObject);
            }
        } else {
            if (targetGameObject != null){
                agent.SetDestination(targetGameObject.transform.position);
            }
        }
        
        TurnTowardsMovement();
    }   

    public void GoToGameObject(GameObject gameObjectTarget){
        targetGameObject = gameObjectTarget;
        agent.SetDestination(targetGameObject.transform.position);

        ////////////////////
        // // Trade
        // Debug.Log("Buying 1 Iron from city");
        // gameObjectTarget.GetComponent<City>().BuyItem("silk", 1, boat);

        // boat.PrintInventory();
        // Debug.Log("City Inventory: " + gameObjectTarget.GetComponent<City>().citySerializable.ToString());
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("Entered Trigger");
        PirateController pirate = hitInfo.GetComponent<PirateController>();
        City city = hitInfo.GetComponent<City>();

        if (pirate && !isSafeInCity){
            Debug.Log("Pirate Found");
            int num_enemies = pirate.boat.baseStats.num_ships;
            ScriptableBoatStats[] enemiesStats = new ScriptableBoatStats[num_enemies];
            for(int i = 0; i < num_enemies; i++){
                enemiesStats[i] = pirate.boat.baseStats;
            }
            // Destroy the pirate
            Destroy(pirate.gameObject);

            // Load the battle scene
            GameManager.Instance.LoadBattle(mapSceneManager, boat, enemiesStats);
        } else if (city){
            Debug.Log("Entered city");
            isSafeInCity = true;
            mapSceneManager.EnterCity(city);
        }

    }

    void TurnTowardsMovement()
    {
        // Check if player is moving and rotate it according to the direction of movement
        if (agent.velocity.sqrMagnitude > 0.001f)
        {
            // Calculate the direction of movement
            Vector2 moveDirection = new Vector2(agent.velocity.x, agent.velocity.y).normalized;
            // Calculate the angle between the current forward direction and the movement direction
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90;
            
            // Rotate the player's ship to face the direction of movement
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void GainGold(int amount){
        boat.gold += amount;
        totalGoldRun += amount;
    }

    public void PayGold(int amount){
        if (amount > boat.gold){
            Debug.LogError("Not enough gold");
        }
        boat.gold -= amount;
    }

    public int GetPlayerGold(){
        return (int)boat.gold;
    }

    public int GetTotalGoldRun(){
        return (int)totalGoldRun;
    }

    public bool IsSafeInCity(){ return isSafeInCity; }
}
