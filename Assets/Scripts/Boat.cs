using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat: MonoBehaviour
{
    // Create a list of dictionaries for the boat inventory
    public List<Dictionary<string, int>> inventory = new List<Dictionary<string, int>>();
    // [{'cotton', 10}, {'silk', 5}]
    public float weight = 0.0f;
    public float maxWeight = 10.0f;
    public float baseSpeed = 1.0f;
    public float speed;

    void Start(){
        //Based on the base speed, weight and max weight, calculate speed
        speed = baseSpeed + (maxWeight - weight)/(2*maxWeight);
    }

}
