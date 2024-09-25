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

public class BoatStatsHelper{
    public static float GetBoatStatsByName(BoatStats stats, string attName){
        switch(attName){
            case "hp":
                return stats.hp;
            case "speed":
                return stats.speed;
            case "manuverability":
                return stats.manuverability;
            case "num_canons":
                return stats.num_canons;
            case "maxWeight":
                return stats.maxWeight;
            case "visibility":
                return stats.visibility;
            case "weight":
                return stats.weight;
            case "cooldown":
                return stats.cooldown;
        }
        Debug.LogError("Attribute not found: " + attName);
        return 0.0f;
    }

    public static BoatStats SetBoatStatsByName(BoatStats stats, string attName, float value){
        switch(attName){
            case "hp":
                stats.hp = value;
                return stats;
            case "speed":
                stats.speed = value;
                return stats;
            case "manuverability":
                stats.manuverability = value;
                return stats;
            case "num_canons":
                stats.num_canons = (int)value;
                return stats;
            case "maxWeight":
                stats.maxWeight = value;
                return stats;
            case "visibility":
                stats.visibility = value;
                return stats;
            case "weight":
                stats.weight = value;
                return stats;
            case "cooldown":
                stats.cooldown = value;
                return stats;
        }
        Debug.LogError("Attribute not found: " + attName);
        return stats;
    }

}