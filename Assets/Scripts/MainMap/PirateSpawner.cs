using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawner : MonoBehaviour
{
    [SerializeField] private MapSceneManager mapSceneManager;
    [SerializeField] private MapController mapController;
    [SerializeField] private GameObject mapPiratePrefab;
    [SerializeField] private PlayerController player;


    // Spawning parameters
    [SerializeField] private int maxPirates;          // Max number of pirates alive at the same time
    [SerializeField] private float spawnInterval;   // Time interval between pirate spawns
    [SerializeField] private float playerSpawnRadius; 
    [SerializeField] private int maxLevel;

    private float spawnTimer;
    private List<GameObject> spawnedPirates = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerBoat").GetComponent<PlayerController>();
        mapController = GameObject.Find("World").GetComponent<MapController>();
        spawnTimer = spawnInterval;  // Initialize spawn timer
    }

    // Update is called once per frame
    void Update()
    {
        // Reduce spawn timer
        spawnTimer -= Time.deltaTime;

        // Check if we can spawn a new pirate
        if (spawnTimer <= 0 && CountAlivePirates() < maxPirates)
        {
            SpawnPirate();
            spawnTimer = spawnInterval;  // Reset timer
        }
    }

    // Count the pirates that are still alive (not destroyed)
    private int CountAlivePirates()
    {
        RemoveNullPirates();  // Remove null objects from the list
        return spawnedPirates.Count;
    }

    // Spawn a new pirate at a random position within the spawn radius
    private void SpawnPirate()
    {
        Vector2 randomPosition;
        while(true){
            randomPosition = new Vector2(
                Random.Range(-mapController.mapSize.x*0.4f, mapController.mapSize.x*0.4f), 
                Random.Range(-mapController.mapSize.y*0.4f, mapController.mapSize.y*0.4f)
            );
            if (
                mapController.IsWater(randomPosition) && 
                Vector2.Distance(randomPosition, player.transform.position) > playerSpawnRadius
            ) { break; }
        }


        Vector3 spawnPosition = new Vector3(randomPosition.x, randomPosition.y, 0f);

        GameObject newPirate = Instantiate(mapPiratePrefab, spawnPosition, Quaternion.identity);
        spawnedPirates.Add(newPirate);
        newPirate.transform.parent = transform;
        int days = mapSceneManager.GetDaysPassed();
        // Chose a randomly a level based on the number of days passed
        int level = (int)Random.Range(1f, (float)(days + 3)/4);
        if (level < 1) { level = 1; }
        if (level > maxLevel) { level = maxLevel; }
        newPirate.GetComponent<Boat>().SetLevel(level);
    }

    // Remove pirates from the list that have been destroyed (null references)
    private void RemoveNullPirates()
    {
        spawnedPirates.RemoveAll(pirate => pirate == null);
    }
}
