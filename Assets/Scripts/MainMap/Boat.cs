using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat: MonoBehaviour
{
    readonly Dictionary<string, float> item_weights = new Dictionary<string, float>()
    {
        {"iron", 5f}, {"cotton", 1f}, {"silk", 0.5f}, {"wood", 2f}, {"grain", 1f}
    };

    public ScriptableBoatStats[] possiblePirates;
    public ScriptableBoatStats baseStats;
    public BoatStats boatStats;

    public int gold = 1000;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();

    void Start(){
        boatStats = baseStats.baseStats;
        //Based on the base speed, weight and max weight, calculate speed
        boatStats.speed = baseStats.baseStats.speed + (boatStats.maxWeight - boatStats.weight)/(2*boatStats.maxWeight);

        inventory = new Dictionary<string, int>(){ {"iron", 0}, {"cotton", 10}, {"silk", 2}, {"wood", 0}, {"grain", 3} }; 
        PrintInventory();
    }

    public void PrintInventory(){
        String message = "Boat Inventory: \n";
        foreach (var pair in inventory)
        {
            message += String.Format("{0}: {1}\n", pair.Key, pair.Value);
        }

        Debug.Log(message);
    }

    public void SetLevel(int level){
        ScriptableBoatStats scriptableBoatStats = possiblePirates[level - 1];
        if (scriptableBoatStats == null){
            Debug.Log("Error: ScriptableBoatStats not found");
            return;
        }
        baseStats = scriptableBoatStats;
        boatStats = scriptableBoatStats.baseStats;
        boatStats.speed = baseStats.baseStats.speed + (boatStats.maxWeight - boatStats.weight)/(2*boatStats.maxWeight);
        Debug.Log("Pirate Base Stats: " + baseStats.ToString());
    }
}
