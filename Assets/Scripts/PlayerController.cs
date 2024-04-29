using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    NavMeshAgent agent;
    public Boat boat;


    // Start is called before the first frame update
    void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = boat.speed;
    }

    // Update is called once per frame
    void Update(){
        agent.speed = boat.speed;
        if (Input.GetMouseButtonDown(0)){
            //Check if clicked on a tilemap
            RaycastHit2D hit = Physics2D.GetRayIntersection(cam.ScreenPointToRay(Input.mousePosition), 100f);
            if (hit.transform == null){
                // Get the mouse position in world space
                Vector3 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                worldPoint.z = 0;
                Debug.Log("World Point: " + worldPoint);
                // Debug.Log("Agent Position: " + transform.position);
                agent.SetDestination(worldPoint);
            }            
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

    public void GoToGameObject(GameObject gameObject){
        agent.SetDestination(gameObject.transform.position);

        ////////////////////
        // Trade
        Debug.Log("Buying 1 Iron from city");
        gameObject.GetComponent<City>().BuyItem("silk", 1, boat);

        boat.PrintInventory();
        Debug.Log("City Inventory: " + gameObject.GetComponent<City>().citySerializable.ToString());
    }
}
