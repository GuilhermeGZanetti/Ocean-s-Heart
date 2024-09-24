using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{
    public float speed_up = 4f;
    [SerializeField] private PlayerController player;
    [SerializeField] private float dayDuration;

    private float daysPassed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //When num keys down change timescale
        if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            Time.timeScale = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            Time.timeScale = speed_up;
        }

        daysPassed += Time.deltaTime/dayDuration;
    }

    public int GetDaysPassed(){
        return (int)daysPassed;
    }

    public void ReturnFromBattle(MapSceneData mapSceneData){
        // Load data to be passed to mapscene
        player.boat.boatStats = mapSceneData.playerStats;
        if (mapSceneData.playerLostBattle){
            Debug.Log("Player LOST!!");
        } else {
            Debug.Log("Player won battle!");
            player.GetGold((int)mapSceneData.looted_gold);
        }
    }
}
