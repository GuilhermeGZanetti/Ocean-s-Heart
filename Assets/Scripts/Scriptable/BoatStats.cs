using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoatStats", menuName = "Stats/BoatStats")]
[Serializable]
public class BoatStats : ScriptableObject
{
    public float hp = 100f;
    public float baseSpeed = 1f;
    public float speed = 1f;
    public int num_canons = 1;

    public float weight = 0.0f;
    public float maxWeight = 10.0f;
}