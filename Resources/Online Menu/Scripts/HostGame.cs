using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour 
{
	private uint roomSize = 4;
	private string roomName;
	private string roomPassword;
	private NetworkManager networkManager;
	public GameObject errorMessager;

	void Start () 
	{
		roomPassword = "";
		errorMessager.SetActive (false);
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null)
			networkManager.StartMatchMaker ();
	}

	public void SetRoomName(string name)
	{
		roomName = name;
	}

	public void SetPassword(string password)
	{
		roomPassword = password;
	}

	public void CreateRoom()
	{
		if (roomName != null && roomName != "")
		{
			Debug.Log ("Creating Room :" + roomName);
			networkManager.matchMaker.CreateMatch (roomName, roomSize, true, "", roomPassword, "", 0, 0,networkManager.OnMatchCreate);
		}
		else
			WriteErrorMessage("Error creating room : must enter a name");
	}

	public void WriteErrorMessage(string message)
	{
		errorMessager.gameObject.SetActive (true);
		errorMessager.GetComponent<Text> ().text = message;
	}
}
