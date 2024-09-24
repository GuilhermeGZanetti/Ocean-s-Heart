using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class FloatingTextController : MonoBehaviour
{
    public Transform target; // The enemy boat to follow
    public Vector3 offset; // Offset to position the text above the enemy

    [SerializeField] private TextMeshProUGUI floatingText;
    [SerializeField] private HealthBarController healthBar;

    // Start is called before the first frame update
    void Awake()
    {
        healthBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Set the position of the text to follow the boat
            Vector3 screenPos = target.position + offset;
            transform.position = screenPos;
        }
    }

    public void SetTarget(Transform my_target){
        target = my_target;

        BattleBoat boat = target.GetComponent<BattleBoat>();
        if (boat) {
            healthBar.gameObject.SetActive(true);
            healthBar.SetBattleBoat(boat);
        }
    }

    // Optional: Set the text content dynamically
    public void SetText(string text)
    {
        floatingText.text = text;
    }
}
