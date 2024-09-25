using System;
using UnityEngine;

[Serializable]
public class BattleSceneData
{
    public BoatStats playerStats;
    public BoatStats baseStats;
    public ScriptableBoatStats[] enemiesStats;
}