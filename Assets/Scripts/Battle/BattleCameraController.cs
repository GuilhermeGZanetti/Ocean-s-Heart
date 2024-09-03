using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BattleCameraController : MonoBehaviour
{
    public float zoomSpeed = 1f;
    public float panSpeed = 10f;
    
    private Camera mainCamera;
    private Vector3 lastPanPosition;
    [SerializeField] private float zoomAmount = 1f;
    [SerializeField] private float maxOrthographicSize;

    private BattleSceneManager battleSceneManager;
    private Tilemap oceanTilemap;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        ZoomCamera(0);
        // maxOrthographicSize = mainCamera.orthographicSize;
        battleSceneManager = GameObject.Find("SceneManager").GetComponent<BattleSceneManager>();
        oceanTilemap = GameObject.Find("Ocean").GetComponent<Tilemap>();
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

        // Centralize the camera between all boats
        Vector3 centralizedPosition = battleSceneManager.GetCentralizedPosition();
        // Lerp the camera to the centralized position
        transform.position = Vector3.Lerp(transform.position, new Vector3(centralizedPosition.x, centralizedPosition.y, transform.position.z) , 0.01f);
    }

    private void ZoomCamera(float scrollAmount)
    {
        zoomAmount += scrollAmount * zoomSpeed;
        zoomAmount = Mathf.Clamp(zoomAmount, 1.0f, 5f); // Limit zoom amount if needed

        //Set camera transform z to zoomAmount
        mainCamera.orthographicSize = maxOrthographicSize/zoomAmount;
    }

    private void PanCamera()
    {
        Vector3 panPosition = Input.mousePosition;
        Vector3 panOffset = lastPanPosition - panPosition;

        Vector2 mapSize = new Vector2(oceanTilemap.size.x, oceanTilemap.size.y) * oceanTilemap.cellSize.x;

        // Clamp position to the map size
        Vector3 clampedPosition = transform.position + panOffset * panSpeed / zoomAmount;;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -mapSize.x / 2f, mapSize.x / 2f);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -mapSize.y / 2f, mapSize.y / 2f);
        transform.position = clampedPosition;


        lastPanPosition = panPosition;
    }
}
