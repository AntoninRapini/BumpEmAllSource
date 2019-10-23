using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LobbyManager_Local : MonoBehaviour {

    public static LobbyManager_Local lobbyManager;
    public GameObject playerSelect, levelSelect;
 
	// Use this for initialization
	void Awake()
	{
        if(lobbyManager == null)
        {
            lobbyManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}
    public void LaunchGame()
    {
        Destroy(LoadSkins.loadSkins.gameObject);
        SceneManager.LoadScene("Game");
    }

	public void BackToMainMenu()
	{
		Destroy(LoadSkins.loadSkins.gameObject);
		SceneManager.LoadScene("Menu");
	}


    void Start()
    {
        if(GameInformations.gameInformations.playerInstanceList.Count > 0)
        {
            playerSelect.SetActive(false);
            levelSelect.SetActive(true);
        }
        else
        {
            playerSelect.SetActive(true);
            levelSelect.SetActive(false);
        }
    }
}
