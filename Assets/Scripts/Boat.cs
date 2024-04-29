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

    public int money = 1000;
    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public float weight = 0.0f;
    public float maxWeight = 10.0f;
    public float baseSpeed = 1.0f;
    public float speed;

    void Start(){
        //Based on the base speed, weight and max weight, calculate speed
        speed = baseSpeed + (maxWeight - weight)/(2*maxWeight);

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
