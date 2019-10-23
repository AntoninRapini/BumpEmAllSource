using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

	private List<GameObject> roomList = new List<GameObject> ();

	[SerializeField]
	private GameObject roomListItemPrefab;

	[SerializeField]
	private Text status;

	[SerializeField]
	private Transform roomListParent;

	private NetworkManager networkManager;
	// Use this for initialization
	void Start () 
	{
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null)
		{
			networkManager.StartMatchMaker ();
		}
		RefreshServerList ();
	}

	public void RefreshServerList()
	{
		networkManager.matchMaker.ListMatches (0, 20, "", true, 0, 0, OnMatchList);
		status.text = "Loading server list...";
	}

	public void OnMatchList(bool success, string extendedInfo, List <MatchInfoSnapshot> matches)
	{
		if (!success)
			status.text = "Failed to load server list";
		else
		{
			ClearServerList ();
			status.text = "";
			foreach (MatchInfoSnapshot match in matches)
			{
				GameObject _roomListItemGO = Instantiate(roomListItemPrefab);
				_roomListItemGO.transform.SetParent (roomListParent);
				roomList.Add (_roomListItemGO);
			}
			if (roomList.Count == 0)
				status.text = "No servers at the moment";
		}
	}

	void ClearServerList()
	{
		for (int i = 0; i < roomList.Count; i++)
		{
			Destroy (roomList [i]);
		}
		roomList.Clear();
	}
}
