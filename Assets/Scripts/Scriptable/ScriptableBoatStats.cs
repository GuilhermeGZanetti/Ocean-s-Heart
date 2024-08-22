using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoatStats", menuName = "Stats/ScriptableBoatStats")]
[Serializable]
public class ScriptableBoatStats : ScriptableObject
{
    public BoatStats baseStats;
    public float lootable_gold;
}


[Serializable]
public struct BoatStats{
    public float hp;
    public float speed;
    public float manuverability;
    public int num_canons;

    public float weight;
    public float maxWeight;
}