using UnityEngine;
using System.Collections;

public class Player_RealGame : Player {

	// Player variables


	public Transform torsoSkinHolder, headSkinHolder, weaponSkinHolder;

	private GameManager gameManager;
	private Shop shop;

	public bool PCplayer;

	public override void LoadVariables()
	{	
		playerNumber = playerInstance.playerNumber;
		gameManager = GameManager.gameManager;
		base.LoadVariables ();
		money = 500;
		spawnLocation = gameManager.GetSpawnLocation (playerNumber);
		LoadSkins ();
		LoadCursor ();

	}

	private void LoadCursor()
	{
		if(PCplayer)
		{
			playerCursor = Instantiate (Resources.Load ("Players/Prefab/Online/Cursor_Online")) as GameObject;
		}
		else
		{
			playerCursor = Instantiate (Resources.Load ("Players/Prefab/Local/Cursor_Local")) as GameObject;
		}

		playerController.playerCursor = playerCursor;
		playerCursor.GetComponent<CursorController> ().player = transform;
		transform.Find("Circle").GetComponent<MeshRenderer>().material = Resources.Load("UI/Game/PlayerUI/Materials/Circles/Circle" + playerNumber.ToString()) as Material;
		spellManager.cursor = playerCursor;
	}


	private void LoadSkins()
	{

		GameObject currentSkin = null;
		if(playerInstance.torsoSkin != null)
		{
			if(playerInstance.torsoSkin.name != "Default")
			{
				currentSkin = Instantiate(playerInstance.torsoSkin);
				currentSkin.transform.SetParent (torsoSkinHolder);
				currentSkin.transform.localPosition = Vector3.zero;
				currentSkin.transform.localRotation = Quaternion.identity;
			}

			if(playerInstance.weaponSkin.name != "Default")
			{
				currentSkin = Instantiate(playerInstance.weaponSkin);
				currentSkin.transform.SetParent (weaponSkinHolder);
				currentSkin.transform.localPosition = Vector3.zero;
				currentSkin.transform.localRotation = Quaternion.identity;
			}

			if(playerInstance.headSkin.name != "Default")
			{
				currentSkin = Instantiate(playerInstance.headSkin);
				currentSkin.transform.SetParent (headSkinHolder);
				currentSkin.transform.localPosition = Vector3.zero;
				currentSkin.transform.localRotation = Quaternion.identity;
			}
		}

	}

	public override void Reset()
	{
		playerCursor.transform.position = transform.position;
		base.Reset ();
	}


	public override void LoadShop(Shop shop)
	{
		shop.player = this;
		shop.spellManager = spellManager;
		for(int i = 0; i < spellManager.ChosenSpells.Length; i++)
		{
			if(spellManager.ChosenSpells[i] != null)
			{
				shop.ownedSpells.Add (spellManager.ChosenSpells [i]);
			}

		}
	}

	public override void Die()
	{
		Reset();
		GameManager.gameManager.alivePlayers.Remove (gameObject);
		for(int i = 0; i < GameManager.gameManager.deadPlayers.Length; i++)
		{
			if(GameManager.gameManager.deadPlayers[i] == null)
			{
				GameManager.gameManager.deadPlayers[i] = gameObject;
				break;
			}
		}
		if (killer != null && killer != gameObject)
		{
			killer.GetComponent<Player> ().GainMoney (50);
		}

		if(GameManager.gameManager.alivePlayers.Count <= 1)
		{
			gameManager.EndRound ();
		}


		playerUIController.playerUI.gameObject.SetActive (false);
		gameObject.SetActive(false);

	}

	public override void Spawn()
	{
		GameManager.gameManager.alivePlayers.Add (gameObject);
		playerUIController.playerUI.gameObject.SetActive (true);
	}

	public override void GainMoney(int _money)
	{
		money += _money;
		playerUIController.UpdateMoney ();
	}

}
