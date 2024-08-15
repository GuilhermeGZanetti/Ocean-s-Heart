using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public float panSpeed = 10f;
    public MapController mapController;
    
    private Camera mainCamera;
    private Vector3 lastPanPosition;
    private float zoomAmount = 1f;
    private float initOrthographicSize;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        initOrthographicSize = mainCamera.orthographicSize;
    }

    private void Update()
    {
        // Zoom with mouse wheel
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0f)
        {
            ZoomCamera(scrollWheelInput);
        }

        // Pan with clicking mouse wheel
        if (Input.GetMouseButtonDown(2)) // 2 is the middle mouse button
        {
            lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2))
        {
            PanCamera();
        }
    }

    private void ZoomCamera(float scrollAmount)
    {
        zoomAmount += scrollAmount * zoomSpeed;
        zoomAmount = Mathf.Clamp(zoomAmount, 1.0f, 5f); // Limit zoom amount if needed

        //Set camera transform z to zoomAmount
        mainCamera.orthographicSize = initOrthographicSize/zoomAmount;
    }

    private void PanCamera()
    {
        Vector3 panPosition = Input.mousePosition;
        Vector3 panOffset = lastPanPosition - panPosition;

        // Clamp position to the map size
        Vector3 clampedPosition = transform.position + panOffset * panSpeed / zoomAmount;;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -mapController.mapSize.x / 2f, mapController.mapSize.x / 2f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -mapController.mapSize.y / 2f, mapController.mapSize.y / 2f);
        transform.position = clampedPosition;


        lastPanPosition = panPosition;
    }
}
