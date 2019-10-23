using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnlineLobbyManager : MonoBehaviour {

	public List<PlayerInstance_Online> players = new List<PlayerInstance_Online> ();


	public void StartGame()
	{
		// Launch Scene set online gameinformations dontdestroyonload
	}



	public void AssignPlayerNumber(PlayerInstance_Online instance)
	{
		if(players.Count < 4)
		{
			players.Add (instance);
			instance.playerNumber = players.Count;
		}
		else
		{
			Destroy (instance.gameObject);
		}
	
	}
}
