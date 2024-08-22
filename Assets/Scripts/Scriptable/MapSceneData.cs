using System;
using UnityEngine;

[Serializable]
public class MapSceneData
{
    public bool playerLostBattle;
    public BoatStats playerStats;
    public float looted_gold;
    public InventorySerializable loot;
}