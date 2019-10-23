using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviour {


	public static GameManager gameManager;

	// Rounds
	public int maxRound = 10;
    public int currentRound = 1;
	private float roundStartTime;

	// Players
	public GameObject[] players;
	public  List<GameObject> alivePlayers = new List<GameObject> ();
    public  GameObject[] deadPlayers;
	public List<Player> playersReady  = new List<Player> ();


	public int[] goldGains = new int[] {500,300,250,100};
	[HideInInspector]
	public bool mortSubite;
	//private int goldAssignation;

	public GameObject playerPrefab;

	private bool playing;

	private GameInformations gameInformations;
	private GameUIManager gameUIManager;
	private LevelManager levelManager;

	void Awake()
	{
		if(gameManager == null)
		{
			gameManager = this;
		}
		else
		{
			Destroy (gameObject);
		}

		if(GameInformations.gameInformations == null)
		{
			GameObject gameInfos = Instantiate (Resources.Load ("GameManagers/Prefabs/LocalGameInformations") as GameObject);
			gameInformations = gameInfos.transform.GetComponent<GameInformations> ();
			LoadDefault ();

		}
		else
		{
			gameInformations = GameInformations.gameInformations;
		
		}
	
		gameInformations.SpawnLevel ();
	}
		

	void Start ()
    {
		gameUIManager = GameUIManager.gameUIManager;
		levelManager = LevelManager.levelManager;
		gameUIManager.LoadVariables ();
        InitializeGame();
		gameUIManager.GameStart ();
    }


	void Update()
	{
			if(playing)
			{
				roundStartTime += Time.deltaTime;
				gameUIManager.UpdateTime (roundStartTime);
			}
	}

	// Spawns the  level and creates the players
	public void InitializeGame()
	{
		players = new GameObject[gameInformations.playerInstanceList.Count];
		for(int i = 0; i <	gameInformations.playerInstanceList.Count; i++)
		{
			GameObject currentPlayer = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			currentPlayer.GetComponent<Player> ().playerInstance = gameInformations.playerInstanceList [i];
			currentPlayer.name = "Player " + gameInformations.playerInstanceList [i].playerNumber;
			currentPlayer.GetComponent<Player>().LoadVariables();
			currentPlayer.SetActive (false);
			players [i] = currentPlayer;
		}
		gameUIManager.LoadShops ();
		deadPlayers = new GameObject[players.Length - 1];
	}



	public void StartRound()
	{
		for(int i = 0; i < players.Length; i++)
		{
			levelManager.SpawnPositions [i].SetActive (true);
		}
		mortSubite = false;
		gameUIManager.RoundStart ();
	}


    public void EndRound()
    {
		
		roundStartTime = 0;
		currentRound++;
        playing = false;
        GameObject winner = alivePlayers [0];
		winner.transform.GetComponent<Player> ().roundWon++;
        
		for (int i = 0; i < players.Length; i++)
		{
			players [i].GetComponent<Player> ().Reset ();
			players [i].SetActive (false);
		}
		if(currentRound > maxRound)
		{
			EndGame ();
		}
		else
		{
			AssignGold ();
			gameUIManager.RoundEnd ();
		}
    }


    public void EndGame()
    {
		gameUIManager.GameEnd ();
		StartCoroutine ("QuitGame");
		alivePlayers.Clear ();
		for(int i = 0; i < deadPlayers.Length; i++)
        {
            deadPlayers[i] = null;
        }
    }

 	IEnumerator QuitGame()
	{
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Menu");
	}


	public void MortSubite()
	{
		mortSubite = true;
		for(int i = 0; i < players.Length;i++)	
		{
			players [i].GetComponent<Player> ().pushResistance = 0;
		}
		gameUIManager.MortSubite ();
	}

	public void SpawnPlayers()
	{
		for (int i = 0; i < players.Length; i++)
		{
			players[i].SetActive(true);
		}
		playing = true;
		roundStartTime = 0;
	}


	public Vector3 GetSpawnLocation(int playerNumber)
	{
		return levelManager.SpawnPositions[playerNumber - 1].transform.position;
	}

	public void AssignGold()
	{
		int i;
		for(i = 0; i < deadPlayers.Length; i++)
		{
			deadPlayers[i].GetComponent<Player>().goldGained = goldGains[i];
			deadPlayers[i].GetComponent<Player>().GainMoney (goldGains[i]);
		}

		if(alivePlayers[0] != null)
		{
			alivePlayers[0].GetComponent<Player>().goldGained = goldGains [i];
			alivePlayers[0].GetComponent<Player>().GainMoney (goldGains[i]);
		}
	}

	void LoadDefault()
	{
		gameInformations.levelPrefab = Resources.Load ("Levels/Level 1/Prefabs/Level1") as GameObject;
		CreatePlayer (2);
	}

	void CreatePlayer(int numberOfPlayers)
	{
		for(int i = 1; i <= numberOfPlayers; i++)
		{
			if (i > 5)
				break;

			PlayerInstance_Local newPlayer = new PlayerInstance_Local ();
			newPlayer.weaponSkin = Resources.Load ("Skins/Prefabs/Weapon/Default")  as GameObject;
			newPlayer.headSkin = Resources.Load ("Skins/Prefabs/Head/Default") as GameObject;
			newPlayer.torsoSkin = Resources.Load ("Skins/Prefabs/Torso/Default")  as GameObject;
			newPlayer.playerNumber = i;
			gameInformations.playerInstanceList.Add (newPlayer);
		}

	}
}
