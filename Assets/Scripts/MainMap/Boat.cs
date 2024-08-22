using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Boat: MonoBehaviour
{
    readonly Dictionary<string, float> item_weights = new Dictionary<string, float>()
    {
        {"iron", 5f}, {"cotton", 1f}, {"silk", 0.5f}, {"wood", 2f}, {"grain", 1f}
    };

    public BoatStats boatStats;

    public int money = 1000;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();

    void Start(){
        //Based on the base speed, weight and max weight, calculate speed
        boatStats.speed = boatStats.baseSpeed + (boatStats.maxWeight - boatStats.weight)/(2*boatStats.maxWeight);

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
}
