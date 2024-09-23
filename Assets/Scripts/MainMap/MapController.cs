using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    private Tilemap tilemapOcean;
    private Tilemap tilemapLand;
    public Vector2 mapSize = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        //Get two children game objects with tilemaps
        tilemapOcean = transform.Find("Ocean").GetComponent<Tilemap>();
        if (tilemapOcean == null)
        {
            Debug.LogError("Could not find Ocean tilemap!");
        }

        tilemapLand = transform.Find("Land").GetComponent<Tilemap>();
        if (tilemapLand == null)
        {
            Debug.LogError("Could not find Land tilemap!");
        }

        //Set mapSize equal to the tilemap size
        mapSize = new Vector2(tilemapOcean.size.y, tilemapOcean.size.x);
        mapSize = mapSize * tilemapOcean.cellSize.x;
        Debug.Log("Map Size: " + mapSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsWater(Vector2 position){
        return tilemapOcean.HasTile(tilemapOcean.WorldToCell(position)) &&
                !tilemapLand.HasTile(tilemapLand.WorldToCell(position));
    }
}
