using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CickHandlerCity : MonoBehaviour

{
    public float hover_increase = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        // Set scale to 1, 1, 1
        transform.localScale = Vector3.one;
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
