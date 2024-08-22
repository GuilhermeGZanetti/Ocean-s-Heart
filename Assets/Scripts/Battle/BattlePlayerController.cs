using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattlePlayerController : MonoBehaviour
{

    private BattleBoat boat;
    [SerializeField] private float sailSpeed = 5.0f;

    // Start is called before the first frame update
    void Start(){
        boat = GetComponent<BattleBoat>();
    }

    // Update is called once per frame
    void Update(){
        boat.SetTargetRudderPosition(Input.GetAxisRaw("Horizontal"), Time.deltaTime);
        if (Input.GetAxisRaw("Vertical") != 0) {
            boat.ChangeSailStatus(Input.GetAxisRaw("Vertical") * sailSpeed * Time.deltaTime);
        }
        if (Input.GetButtonDown("Fire1")) {
            boat.ShootCanons(new Vector2(1, 0));
        }
        if (Input.GetButtonDown("Fire2")) {
            boat.ShootCanons(new Vector2(-1, 0));
        }
    }   


    public BoatStats GetBoatStats(){
        return boat.boatStats;
    }



}
