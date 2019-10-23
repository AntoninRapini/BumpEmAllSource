using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Pause_Local : MenuClass {


	protected override void Update () 
	{
		if(input.Current.Up)
		{
			Debug.Log ("up");
			DownArray();
		}
		else if(input.Current.Down)
		{
			Debug.Log ("down");
			UpArray ();
		}
		if(input.Current.Select)
		{
			Submit ();
		}
		if(input.Current.Deselect)
		{
			Resume ();
		}
	}

	public void BackToMenu()
	{
		GameUIManager.gameUIManager.UnPauseGame ();
		SceneManager.LoadScene ("LocalLobby");
	}

	public void Resume()
	{
		GameUIManager.gameUIManager.UnPauseGame ();
	}


	public void Quit()
	{
		GameUIManager.gameUIManager.UnPauseGame ();
		Application.Quit();
	}
}
