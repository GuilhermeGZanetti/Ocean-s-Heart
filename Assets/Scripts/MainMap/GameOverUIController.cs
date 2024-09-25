using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalGold;

    public void SetTotalGold(int gold)
    {
        totalGold.text = gold.ToString();
    }
}
