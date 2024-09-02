using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private BattleSceneData battleSceneData;


    [SerializeField] private BattlePlayerController player;
    [SerializeField] private float enemy_spacing = 1f;
    private List<GameObject> enemies;


    private float looted_gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        looted_gold = 0;
        Time.timeScale = 1f;

        //Read battle info from GameManager
        battleSceneData = GameManager.Instance.battleSceneData;
        Debug.Log("BattleSceneData: " + battleSceneData);

        //Load enemies
        enemies = new List<GameObject>();

        float i = -battleSceneData.enemiesStats.Length/2*enemy_spacing;
        foreach (ScriptableBoatStats enemyStats in battleSceneData.enemiesStats){
            GameObject enemy = Instantiate(enemyStats.boatPrefab, new Vector3(i, 3.0f, 0.27f), Quaternion.identity);
            enemy.GetComponent<BattleBoat>().boatStats = enemyStats.baseStats;
            enemies.Add(enemy);
            i += enemy_spacing;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BoatDestroyed(GameObject destroyedBoat){
        if (destroyedBoat.tag == "Player"){
            MapSceneData mapSceneData = new MapSceneData
            {
                playerLostBattle = true,
                looted_gold = 0,
                loot = new InventorySerializable(),
                playerStats = destroyedBoat.GetComponent<BattleBoat>().boatStats
            };

            ReturnFromBattle(mapSceneData);
        } else {
            // Remove enemy from enemies list
            Debug.Log("Enemy destroyed: " + destroyedBoat.name);
            Debug.Log("Destroyed obj: " + destroyedBoat);
            Debug.Log("Enemies: " + enemies);
            Debug.Log("Enemy: " + enemies[0]);

            enemies.Remove(destroyedBoat);
            looted_gold += destroyedBoat.GetComponent<BattleBoat>().GetLootedGold();
            Debug.Log("Enemies: " + enemies.Count);

            // Check if all enemies are destroyed
            if (enemies.Count == 0){
                // Load MapScene
                MapSceneData mapSceneData = new MapSceneData
                {
                    playerLostBattle = false,
                    looted_gold = looted_gold,
                    loot = new InventorySerializable(),
                    playerStats = player.GetBoatStats()
                };
                ReturnFromBattle(mapSceneData);
            };
        }
        
    }

    public void ReturnFromBattle(MapSceneData mapSceneData){
        // Load data to be passed to mapscene
        GameManager.Instance.LoadMap(mapSceneData);
    }
}
