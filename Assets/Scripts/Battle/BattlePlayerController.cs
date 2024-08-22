using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattlePlayerController : MonoBehaviour
{

    private BattleBoat boat;
    [SerializeField] private float rudderSpeed = 5.0f;
    [SerializeField] private float sailSpeed = 5.0f;

    // Start is called before the first frame update
    void Start(){
        boat = GetComponent<BattleBoat>();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetAxisRaw("Horizontal") != 0) {
            boat.rudderPosition += Input.GetAxisRaw("Horizontal") * rudderSpeed * Time.deltaTime;      
            boat.rudderPosition = Mathf.Clamp(boat.rudderPosition, -1.0f, 1.0f);  
        }

        if (Input.GetAxisRaw("Vertical") != 0) {
            boat.sailStatus += Input.GetAxisRaw("Vertical") * sailSpeed * Time.deltaTime;     
            boat.sailStatus = Mathf.Clamp(boat.sailStatus, 0.0f, 1.0f);   
        }
    }   



}
