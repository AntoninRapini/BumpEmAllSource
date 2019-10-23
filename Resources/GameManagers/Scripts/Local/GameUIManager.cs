using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
// Handles Global UI functions, personnal UI  (Shops and player UIs) are handled separately
public class GameUIManager : MonoBehaviour {


	public static GameUIManager gameUIManager;

	// UI Elements
	public GameObject gameUI;
	public GameObject betweenRoundsUI;
	public GameObject scoreBoardUI;
	public GameObject pauseUI;
	public Text leaderBoard;
	public Text roundNumber;
	public Text roundTimer;
	public Text countDown;
	public Text messageText;

	[HideInInspector] List<GameObject> shops = new List<GameObject>();


	private bool gamePaused;
	private GameManager gameManager;
	private LevelManager levelManager;

	void Awake()
	{
		if(gameUIManager == null)
		{
			gameUIManager = this;
		}
		else
		{
			Destroy (gameObject);
		}
	}

	public void LoadVariables()
	{
		gameManager = GameManager.gameManager;
		levelManager = LevelManager.levelManager;
		UnPauseGame ();
	}
		

	public void GameStart()
	{
		//messageText.GetComponent<Text> ().fontSize = 48;
		leaderBoard.text = "";
		roundNumber.text = "";
		roundTimer.text = "0 :00";
		EnableShops ();
	}

	public void RoundStart()
	{
		roundNumber.text = "Round " + GameManager.gameManager.currentRound;
		leaderBoard.text = "";

		for(int i = 0; i < shops.Count; i++)
		{
			shops [i].SetActive (false);
		}
		SetSpawnPreviewsActive (true);
		betweenRoundsUI.SetActive (false);
		StartCoroutine ("RoundStartCountDown");
	}

	IEnumerator RoundStartCountDown()
	{


		float Timer = 0;
		float countDownTime = 3;

		while(Timer < countDownTime)
		{
			countDown.text = Mathf.Floor (countDownTime - Timer + 1).ToString() ;
			Timer += Time.deltaTime;
			yield return null;
		}

		for(int i = 0; i < gameManager.players.Length; i++)
		{
			levelManager.SpawnPositions [i].SetActive (false);
		}

		countDown.text = "GO !";
		SetSpawnPreviewsActive (false);
		gameUI.SetActive(true);
		messageText.text = "\n" + "Round left : " +  (GameManager.gameManager.maxRound - GameManager.gameManager.currentRound).ToString ();
		gameManager.SpawnPlayers ();

		yield return new WaitForSeconds (1);

		messageText.text = "";
		countDown.text = "";

	}

	public void RoundEnd()
	{
		StartCoroutine ("ShowScoreBoard");
	}

	public void HandlePause()
	{
		if(gamePaused)
		{
			UnPauseGame ();
		}
		else
		{
			PauseGame ();
		}
	}

	public void PauseGame()
	{
		gamePaused = true;
		Time.timeScale = 0;
		pauseUI.SetActive();
	}

	public void UnPauseGame ()
	{
		gamePaused = false;
		Time.timeScale = 1;
		pauseUI.SetInactive();
	}

	public void GameEnd()
	{
		gameUI.SetActive (false);
		betweenRoundsUI.SetActive (false);
		scoreBoardUI.SetActive (true);
		messageText.GetComponent<Text> ().fontSize = 120;

		GameObject[] temporaryPlayerList = new GameObject[gameManager.players.Length];
		temporaryPlayerList = gameManager.players;

		int yRemove = 0;
		for(int i = 0; i < gameManager.players.Length; i++)
		{
			GameObject winner = null;
			for(int y = 0; y < gameManager.players.Length; y++)
			{
				if(temporaryPlayerList[y] != null)
				{
					if (winner == null)
					{
						winner = temporaryPlayerList [y];
					}
					else
					{
						if(temporaryPlayerList [y].GetComponent<Player_RealGame> ().roundWon > winner.GetComponent<Player_RealGame> ().roundWon)
						{
							winner = temporaryPlayerList [y];
							yRemove = y;
						}

					}
				}


			}
			temporaryPlayerList [yRemove] = null;
			messageText.GetComponent<Text> ().text += "\n"+ (i + 1).ToString() + " : Player " + winner.GetComponent<Player> ().playerNumber;

		}


	}

	public void LoadShops()
	{
		for(int i = 0; i < gameManager.players.Length; i++)
		{
			GameObject newShop = betweenRoundsUI.transform.GetComponent<RoundUIElements> ().playerShops [gameManager.players [i].GetComponent<Player> ().playerNumber - 1];
			shops.Add(newShop);
			Shop currentShop = newShop.GetComponent<Shop> ();
			gameManager.players [i].GetComponent<Player> ().LoadShop (currentShop);
		}
	}

	public void EnableShops()
	{
		betweenRoundsUI.SetActive (true);
		for(int i = 0; i < shops.Count; i++)
		{
			shops [i].SetActive (true);
		}
	}
		
	public void UpdateTime(float roundStartTime)
	{
		string seconds;
		if(Mathf.Floor (roundStartTime % 60) < 10)
		{
			seconds = "0" + Mathf.Floor (roundStartTime % 60).ToString();
		}
		else
		{
			seconds = Mathf.Floor (roundStartTime % 60).ToString();
		}

		if(roundStartTime > 90 && !gameManager.mortSubite)
		{
		 	gameManager.MortSubite ();
		}
		roundTimer.text = Mathf.Floor(roundStartTime/60) + " : " +  seconds;
	}

	public void MortSubite()
	{
		StartCoroutine ("annonceMortSubite");
	}

	IEnumerator annonceMortSubite()
	{
		countDown.text = "Mort Subite !";
		countDown.gameObject.GetComponent<Text> ().text = "  Mort subite !";
		yield return new WaitForSeconds (2);
		countDown.text = "";
	}

	IEnumerator ShowScoreBoard()
	{
		StringBuilder builder = new StringBuilder ();
		roundNumber.text = "";
		countDown.text = "";

		Player_RealGame currentPlayer = gameManager.alivePlayers [0].transform.GetComponent<Player_RealGame> ();
		builder.Append ("\n");
		builder.Append ("Winner : " + "\n" + "Player " + currentPlayer.playerNumber + " +" + currentPlayer.goldGained + " gold");
		builder.Append ("\n\n");

		int y = 1;

		for (int i = gameManager.deadPlayers.Length; i  >  0; i--)
		{
			y++;
			currentPlayer = gameManager.deadPlayers [i - 1].transform.GetComponent<Player_RealGame> ();
			builder.Append ( y + " : Player " + currentPlayer.playerNumber + " +" + currentPlayer.goldGained + " gold");
		}

		messageText.fontSize = 48;
		messageText.text = builder.ToString ();
		yield return new WaitForSeconds (4);
		messageText.text = "";
		betweenRoundsUI.SetActive (true);
		roundNumber.text = "Round " + gameManager.currentRound;

		yield return null;

		for(int i = 0; i < shops.Count; i++)
		{
			shops [i].SetActive (true);
		}

		gameManager.alivePlayers.Clear();

		for(int i = 0; i < gameManager.deadPlayers.Length; i++)
		{
			gameManager.deadPlayers [i] = null;
		}


		GameObject[] temporaryPlayerList = new GameObject[GameManager.gameManager.players.Length];
		builder.Remove (0, builder.Length);
		builder.Append ("Leaderboard");
		temporaryPlayerList = gameManager.players;


		bool inOrder = false;
		GameObject temporaryObject;
		while(!inOrder)
		{
			inOrder = true;
			for(int i = 0; i < temporaryPlayerList.Length - 1; i++)
			{
				if(temporaryPlayerList [i].GetComponent<Player_RealGame> ().roundWon < temporaryPlayerList [i + 1].GetComponent<Player_RealGame> ().roundWon)
				{
					temporaryObject = temporaryPlayerList [i + 1];
					temporaryPlayerList [i + 1] = temporaryPlayerList [i];
					temporaryPlayerList [i] = temporaryObject;
					inOrder = false;
				}

			}
		}

		for(int i = 0; i < temporaryPlayerList.Length; i++)
		{
			builder.Append ("\n" + "Player " + temporaryPlayerList[i].GetComponent<Player> ().playerNumber);
		}
		leaderBoard.text = builder.ToString ();

	}


	public void SetSpawnPreviewsActive(bool active)
	{
		for(int i = 0; i < gameManager.players.Length; i++)
		{
			levelManager.SpawnPositions [gameManager.players [i].GetComponent<Player> ().playerNumber - 1].SetActive (active);
		}
	}


}
