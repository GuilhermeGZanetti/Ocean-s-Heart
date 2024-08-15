using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{
    public float speed_up = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //When num keys down change timescale
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Time.timeScale = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Time.timeScale = speed_up;
        }
    }

    public void ReturnFromBattle(MapSceneData mapSceneData){
        // Load data to be passed to mapscene
    }
}
