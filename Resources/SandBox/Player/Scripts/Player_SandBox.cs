using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_SandBox : Player {



	private SandBoxGameManager gameManager;


	public override void LoadVariables()
	{	
		gameManager = SandBoxGameManager.sandBoxGameManager;
		base.LoadVariables ();
		spawnLocation = gameManager.GetSpawnLocation (playerNumber);
		LoadCursor ();
	}
		
	private void LoadCursor()
	{
		playerCursor = Instantiate (Resources.Load ("Players/Prefab/Online/Cursor_Online"), transform.position, Quaternion.identity) as GameObject;
		playerController.playerCursor = playerCursor;
		playerCursor.GetComponent<CursorController> ().player = transform;
		transform.Find("Circle").GetComponent<MeshRenderer>().material = Resources.Load("UI/Game/PlayerUI/Materials/Circles/Circle" + playerNumber.ToString()) as Material;
		spellManager.cursor = playerCursor;
	}

	public override void Die()
	{
		Reset();
		Spawn ();
	}

	protected override void LoadGameUI()
	{
		playerUIController = transform.GetComponent<PlayerUIController>();
		playerUIController.playerUI = gameManager.gameUIElements.playerUIs[playerNumber - 1].transform.GetComponent<PlayerUI>();
		playerUIController.player = this;
		playerUIController.UpdateMoney ();
	}
}
