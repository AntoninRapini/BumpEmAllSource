using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class SandBoxGameManager : MonoBehaviour {

	public GameObject gameUI;

	public static SandBoxGameManager sandBoxGameManager;
	public int numberOfPlayers;
	public bool PC;
	public GameUIElements gameUIElements;
	public GameObject[] players;
	public GameObject playerPrefab;
	public LevelManager levelManager;
	void Awake()
	{
		if(sandBoxGameManager == null)
		{
			sandBoxGameManager = this;
		}
		else
		{
			Destroy (gameObject);
		}
	}
		
	void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		gameUI.SetActive (true);
		levelManager = LevelManager.levelManager;
		players = new GameObject[numberOfPlayers];
		for(int i = 0; i < numberOfPlayers; i++)
		{
			GameObject newPlayer = Instantiate (playerPrefab);
			newPlayer.GetComponent<Player> ().playerNumber = i + 1;
			newPlayer.GetComponent<Player> ().LoadVariables();
			players [i] = newPlayer;
		}
	}



	void Update()
	{
		if(Input.GetKeyUp("l"))
		{
			LockUnlockMouse ();
		}
	}

	public Vector3 GetSpawnLocation(int playerNumber)
	{
		return levelManager.SpawnPositions[playerNumber - 1].transform.position;
	}

	public void LockUnlockMouse()
	{
		if(!Cursor.visible)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
