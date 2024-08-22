using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// This is really the only blurb of code you need to implement a Unity singleton
	private static GameManager _Instance;
	public static GameManager Instance
	{
		get
		{
			if (!_Instance)
			{
				_Instance = new GameObject().AddComponent<GameManager>();
				// name it for easy recognition
				_Instance.name = _Instance.GetType().ToString();
				// mark root as DontDestroyOnLoad();
				DontDestroyOnLoad(_Instance.gameObject);
			}
			return _Instance;
		}
	}

	// implement your Awake, Start, Update, or other methods here...

    // Map Scene Manager Reference
    public MapSceneManager mapSceneManager = null;

    public BattleSceneData battleSceneData = null;

    public void LoadBattle(MapSceneManager mapSceneManager, BoatStats playerStats, BoatStats[] enemiesStats){

        // Load data to be passed to battlescene
        Instance.battleSceneData = new BattleSceneData
        {
            playerStats = playerStats,
            enemiesStats = enemiesStats
        };
        Instance.mapSceneManager = mapSceneManager;

        // Load Battle Scene additivelly
        SceneManager.LoadSceneAsync(SceneNames.BattleScene.ToString(), LoadSceneMode.Additive);

        // Deactivate MapScene SceneManager game object
        Debug.Log("MapSceneManager: " + Instance.mapSceneManager);
        Instance.mapSceneManager.gameObject.SetActive(false);
        Debug.Log("Instance.MapSceneManager: " + Instance.mapSceneManager);
    }

    public void LoadMap(MapSceneData mapSceneData){
        Debug.Log("Loading Map Scene");

        // Unload Battle Scene if it is loaded
        SceneManager.UnloadSceneAsync(SceneNames.BattleScene.ToString());

        // Reactivate MapScene SceneManager game object
        Instance.mapSceneManager.gameObject.SetActive(true);
        Instance.mapSceneManager.ReturnFromBattle(mapSceneData);        
    }
}

// Enum with scene names
public enum SceneNames
{
    MainMap,
    BattleScene
}