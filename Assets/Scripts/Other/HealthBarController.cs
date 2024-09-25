using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour
{
    public Image lifeBar; // Reference to the UI Image that shows the life bar
    public TextMeshProUGUI healthText;
    private Boat playerBoat; // Reference to the player's script
    private BattleBoat playerBattleBoat; // Reference to the player's script

    void Start()
    {
        if (playerBattleBoat == null){
            playerBoat = GameObject.Find("PlayerBoat").GetComponent<Boat>();
            playerBattleBoat = GameObject.Find("PlayerBoat").GetComponent<BattleBoat>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBoat != null && playerBattleBoat == null){
            // Update the life bar fill amount based on player's current health
            float healthPercentage = playerBoat.boatStats.hp / playerBoat.baseStats.baseStats.hp;
            lifeBar.fillAmount = Mathf.Clamp01(healthPercentage); // Ensure value is between 0 and 1
            healthText.text = playerBoat.boatStats.hp.ToString() + " / " + playerBoat.baseStats.baseStats.hp.ToString();
        } else if (playerBoat == null && playerBattleBoat != null){
            // Update the life bar fill amount based on player's current health
            float healthPercentage = playerBattleBoat.boatStats.hp / playerBattleBoat.scriptableBoatStats.baseStats.hp;
            lifeBar.fillAmount = Mathf.Clamp01(healthPercentage); // Ensure value is between 0 and 1
            healthText.text = playerBattleBoat.boatStats.hp.ToString() + " / " + playerBattleBoat.scriptableBoatStats.baseStats.hp.ToString();
        }
    }

    public void SetBattleBoat(BattleBoat battleBoat){
        playerBoat = null;
        playerBattleBoat = battleBoat;
    }
}
