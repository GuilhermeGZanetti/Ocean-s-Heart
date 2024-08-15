using System;
using UnityEngine;

[Serializable]
public class BattleSceneData : ScriptableObject
{
    public BoatStats playerStats;
    public BoatStats[] enemiesStats;
}