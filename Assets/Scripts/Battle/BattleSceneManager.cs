using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private BattleSceneData battleSceneData;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        //Read battle info from GameManager
        battleSceneData = GameManager.Instance.battleSceneData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnFromBattle(MapSceneData mapSceneData){
        // Load data to be passed to mapscene
    }
}
