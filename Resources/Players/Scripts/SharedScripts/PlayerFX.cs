using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFX : MonoBehaviour {

	public Dictionary<string, GameObject> playerFXs = new Dictionary<string, GameObject> ();
	public GameObject[] temporaryFXarray;

	void Start()
	{
		for(int i = 0; i < temporaryFXarray.Length; i++)
		{
			playerFXs.Add (temporaryFXarray [i].name, temporaryFXarray [i]);
		}
	}


	public void PlayFX(string fxName)
	{
		if(playerFXs.ContainsKey(fxName))
		{
			playerFXs [fxName].SetActive (true);
		}

	}

	public void StopFX(string fxName)
	{
		if(playerFXs.ContainsKey(fxName))
		{
			playerFXs [fxName].SetActive (false);
		}

	}

}
