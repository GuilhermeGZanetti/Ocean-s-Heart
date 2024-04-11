using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            // Get the mouse position in world space
            Vector3 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            worldPoint.z = 0;
            Debug.Log("World Point: " + worldPoint);
            Debug.Log("Agent Position: " + transform.position);
            agent.SetDestination(worldPoint);
        }
        
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
}
