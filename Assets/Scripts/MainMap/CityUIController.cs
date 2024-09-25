using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textPlayerGold;
    [SerializeField] PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textPlayerGold.text = "Gold: " + player.GetPlayerGold().ToString();
    }
}
