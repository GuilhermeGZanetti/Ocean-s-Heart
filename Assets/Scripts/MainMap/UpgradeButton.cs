using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private String statName;
    [SerializeField] private bool isRepair = false;
    [SerializeField] private float costMultiplier = 1.0f;
    [SerializeField] private float upgradeIncrease = 1.2f;

    private PlayerController player;
    private Button button;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI statText;

    private int goldCost;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerBoat").GetComponent<PlayerController>();

        button = GetComponentInChildren<Button>();
        costText = button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        // Get component in children with name TextStat
        TextMeshProUGUI[] components = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI component in components){
            if (component.name == "TextStat"){
                statText = component;
                break;
            }
        }

        button.onClick.AddListener(Upgrade);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRepair){
            goldCost = (int)((player.boat.baseStats.baseStats.hp - player.boat.boatStats.hp)*costMultiplier);
            statText.text = player.boat.boatStats.hp.ToString() + " / " + player.boat.baseStats.baseStats.hp.ToString();
        } else if (statName == "cooldown"){
            float stat = BoatStatsHelper.GetBoatStatsByName(player.boat.boatStats, statName);
            goldCost = (int)(costMultiplier/stat);
            statText.text = stat.ToString();
        } else {
            float stat = BoatStatsHelper.GetBoatStatsByName(player.boat.boatStats, statName);
            goldCost = (int)(stat * costMultiplier);
            statText.text = stat.ToString();
        }

        costText.text = costText.text.Split("(")[0] + "(" + goldCost.ToString() + " g)";
    }

    public void Upgrade(){
        // Check if player has enough gold
        if (player.boat.gold >= goldCost){
            player.PayGold(goldCost);
        } else {
            Debug.Log("Not enough gold");
            return;
        }

        if (isRepair){
            player.boat.boatStats.hp = player.boat.baseStats.baseStats.hp;
        } else {
            if (statName == "num_canons"){
                player.boat.boatStats.num_canons += 1;
            }  else if (statName == "hp") {
                float new_stat = player.boat.boatStats.hp * upgradeIncrease;
                ScriptableBoatStats oldStats = player.boat.baseStats;
                ScriptableBoatStats newBaseStats = Instantiate(oldStats);
                newBaseStats.baseStats.hp = new_stat;
                player.boat.baseStats = newBaseStats;
                player.boat.boatStats.hp = Math.Clamp(player.boat.boatStats.hp + (new_stat - oldStats.baseStats.hp), 0, new_stat);
            } else {
                float new_stat = BoatStatsHelper.GetBoatStatsByName(player.boat.boatStats, statName) * upgradeIncrease;
                player.boat.boatStats = BoatStatsHelper.SetBoatStatsByName(player.boat.boatStats, statName, new_stat);
            }
        }
    
    }


}
