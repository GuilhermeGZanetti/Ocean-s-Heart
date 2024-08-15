using System;
using UnityEngine;

[Serializable]
public class MapSceneData : ScriptableObject
{
    public BoatStats playerStats;
    public float looted_gold;
    public InventorySerializable loot;
}