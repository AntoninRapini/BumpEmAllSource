using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameInformations : MonoBehaviour {

	public static GameInformations gameInformations;
    public int numberOfPlayersActive;
	public GameObject levelPrefab;

	public List<PlayerInstance_Local> playerInstanceList = new List<PlayerInstance_Local>();

	void Awake()
	{
        DontDestroyOnLoad(gameObject);
        if (gameInformations == null)
		{
			gameInformations = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
		
	void Start () 
	{
		/*Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;*/
	}

	public void AddPlayer(PlayerInstance_Local currentPlayer)
	{
		if(!playerInstanceList.Contains(currentPlayer))
		{
			playerInstanceList.Add (currentPlayer);
		}
	
	}

	public void RemovePlayer(PlayerInstance_Local currentPlayer)
	{
		if (playerInstanceList.Contains(currentPlayer))
        {
			playerInstanceList.Remove(currentPlayer);
        }
	}

    public void SpawnLevel()
	{
		if(LevelManager.levelManager == null)
		{
			Instantiate (levelPrefab, Vector3.zero, Quaternion.identity);
		}

	}

}

