using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PirateController : MonoBehaviour
{
    public Boat boat;
    public float agentSpeedMultiplier = 0.1f;
    public bool isVisible = false;

    public GameObject floatingTextPrefab; // Assign your FloatingText prefab here
    private GameObject floatingTextInstance;

    private PlayerController player;
    private NavMeshAgent agent;

    private Vector2 patrolTarget;
    private float patrolRadius = 8.0f;  // How far away patrol points can be
    private float patrolCooldown = 5.0f;  // Time before setting a new patrol point
    private float patrolTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerBoat").GetComponent<PlayerController>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        SetNewPatrolTarget();  // Initialize first patrol point

        // Instantiate the floating text
        floatingTextInstance = Instantiate(floatingTextPrefab);
        
        // Set the target (enemy boat) to follow
        FloatingTextController floatingTextController = floatingTextInstance.GetComponent<FloatingTextController>();
        floatingTextController.SetTarget(transform);

        // Optional: Set custom text (e.g., health, name, etc.)
        floatingTextController.SetText("Level "+boat.baseStats.level.ToString());
        floatingTextController.transform.SetParent(transform.parent);

        SetVisibility(Vector2.Distance(transform.position, player.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = boat.boatStats.speed * agentSpeedMultiplier;
        patrolTimer -= Time.deltaTime;

        if (player == null){
            player = GameObject.Find("PlayerBoat").GetComponent<PlayerController>();
        }
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Chase player if within chase threshold
        if ((distanceToPlayer <= boat.boatStats.visibility) && !player.IsSafeInCity())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        SetVisibility(distanceToPlayer);

        TurnTowardsMovement();
    }

    void OnDestroy()
    {
        // Destroy floating text when the boat is destroyed
        if (floatingTextInstance != null)
        {
            Destroy(floatingTextInstance);
        }
    }

    private void Patrol()
    {
        // Check if reached the patrol target, or it's time for a new target
        if (Vector2.Distance(transform.position, patrolTarget) < 1.0f || patrolTimer <= 0)
        {
            SetNewPatrolTarget();
        }

        // Move towards the patrol target
        agent.SetDestination(patrolTarget);
    }

    private void ChasePlayer()
    {
        // Move towards the player's current position
        agent.SetDestination(player.transform.position);
    }

    private void SetNewPatrolTarget()
    {
        // Randomly choose a new point within patrolRadius
        Vector2 randomPoint = Random.insideUnitCircle * patrolRadius;
        patrolTarget = (Vector2)transform.position + randomPoint;

        // Reset patrol timer
        patrolTimer = patrolCooldown;
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

    void SetVisibility(float distanceToPlayer)
    {
        // Get the maximum visibility range from the player's boat stats
        float maxVisibility = player.boat.boatStats.visibility;
        if (distanceToPlayer > maxVisibility){
            isVisible = false;
        } else {
            isVisible = true;
        }
        
        // Calculate the transparency based on the distance
        float transparencyFactor = Mathf.Clamp01(1.0f - Mathf.Pow((distanceToPlayer / maxVisibility), 3.0f));

        // Update the main object and its children's materials with the new transparency
        UpdateRenderersTransparency(GetComponent<Renderer>(), transparencyFactor);

        // Set transparency for all child objects
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in childRenderers)
        {
            UpdateRenderersTransparency(renderer, transparencyFactor);
        }
    }

    void UpdateRenderersTransparency(Renderer renderer, float transparencyFactor)
    {
        if (renderer != null && renderer.material.HasProperty("_Color"))
        {
            Color color = renderer.material.color;
            color.a = transparencyFactor;  // Set alpha based on the distance factor
            renderer.material.color = color;

            // Optionally: Disable the renderer if transparency is too low
            renderer.enabled = transparencyFactor > 0.01f;
        }
    }
}
