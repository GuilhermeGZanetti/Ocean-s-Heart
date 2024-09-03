using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateUIHeader : MonoBehaviour
{
    public TextMeshProUGUI speed_value;
    public TextMeshProUGUI gold_value;
    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set text to Time.timeScale
        speed_value.text = Time.timeScale.ToString() + "x";
        gold_value.text = player.boat.gold.ToString();
    }
}
