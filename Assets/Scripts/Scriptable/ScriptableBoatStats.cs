using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBoatStats", menuName = "Stats/ScriptableBoatStats")]
[Serializable]
public class ScriptableBoatStats : ScriptableObject
{
    public BoatStats baseStats;
    public int level;
    public float lootable_gold;
    public int num_ships;
    public GameObject boatPrefab;
}


[Serializable]
public struct BoatStats{
    public float hp;
    public float speed;
    public float manuverability;
    public int num_canons;
    public float maxWeight;
    public float visibility;

    public float weight;
    public float cooldown;
}