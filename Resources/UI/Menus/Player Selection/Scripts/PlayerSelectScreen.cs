using UnityEngine;
using System.Collections;

public class PlayerSelectScreen : MonoBehaviour {

	PlayerOneMenuInputController input;
	public GameObject levelSelectScreen;
    public PlayerSelectScript[] players = new PlayerSelectScript[4];

    void Start ()
    {
		input = transform.GetComponent<PlayerOneMenuInputController> ();
        if(GameInformations.gameInformations.playerInstanceList.Count > 0)
        {
			Debug.Log ("retrive players");
            RetrievePlayers();
            GameInformations.gameInformations.playerInstanceList.Clear();
        }
        else
        {
            LoadPlayersDefault();
        }
	}

	void Update ()
    {
		if (input.Current.Deselect && GameInformations.gameInformations.numberOfPlayersActive == 0)
		{
			LobbyManager_Local.lobbyManager.BackToMainMenu ();
		}

		if(GameInformations.gameInformations.numberOfPlayersActive == GameInformations.gameInformations.playerInstanceList.Count && GameInformations.gameInformations.playerInstanceList.Count >= 1)
		{
			levelSelectScreen.gameObject.SetActive (true);
			gameObject.SetActive (false);
		}
	}

    public void RetrievePlayers()
    {
        for(int i = 0; i < GameInformations.gameInformations.playerInstanceList.Count; i++)
        {
            PlayerInstance_Local instance = GameInformations.gameInformations.playerInstanceList[i];
            players[instance.playerNumber - 1].RetrieveInstance(instance);
        }
    }

    public void LoadPlayersDefault()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].LoadDefault();
        }
    }
}
	