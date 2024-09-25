using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapSceneManager : MonoBehaviour
{
    public float speed_up = 4f;
    [SerializeField] private PlayerController player;
    [SerializeField] private float dayDuration;
    [SerializeField] private GameObject mapUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject cityUI;


    private float daysPassed = 0f;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        mapUI.SetActive(true);
        cityUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused){
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
    }

    public int GetDaysPassed(){
        return (int)daysPassed;
    }

    public void ReturnFromBattle(MapSceneData mapSceneData){
        // Load data to be passed to mapscene
        player.boat.boatStats = mapSceneData.playerStats;
        if (mapSceneData.playerLostBattle){
            Debug.Log("Player LOST!!");
            mapUI.SetActive(false);
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
            gameOverUI.GetComponent<GameOverUIController>().SetTotalGold(player.GetTotalGoldRun());
        } else {
            Debug.Log("Player won battle!");
            player.GainGold((int)mapSceneData.looted_gold);
        }
    }

    public void ReestartGame(){
        GameManager.Instance.ReestartGame();
    }

    public void EnterCity(City city){
        isPaused = true;
        Time.timeScale = 0f;
        mapUI.SetActive(false);
        cityUI.SetActive(true);  
        TextMeshProUGUI[] texts = cityUI.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI text in texts){
            if (text.name == "TextCityName"){
                text.text = city.cityName;
            }
        }
    }

    public void ExitCity(){
        isPaused = false;
        Time.timeScale = 1f;
        mapUI.SetActive(true);
        cityUI.SetActive(false);
    }
}
