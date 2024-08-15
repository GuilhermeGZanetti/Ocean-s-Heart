using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CickHandlerCity : MonoBehaviour

{
    public float hover_increase = 1.1f;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // Set scale to 1, 1, 1
        transform.localScale = Vector3.one;

        GameObject player = GameObject.Find("PlayerBoat");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown()
    {
        
    }


    public void OnPointerUp()
    {
        
    }


    public void OnPointerClick()
    {
        Debug.Log("City Clicked");
        // Call PlayerController GoToGameObject
        playerController.GoToGameObject(gameObject);
    }


    public void OnPointerEnter()
    {
        // Set scale to a bigger size
        transform.localScale = Vector3.one * hover_increase;
    }


    public void OnPointerExit()
    {
        // Set scale back to 1
        transform.localScale = Vector3.one;
    }
}
