using UnityEngine;
using System.Collections;

public class OnlineGameInformations : MonoBehaviour {

	public static OnlineGameInformations onlineGameInformations;
	public int numberOfPlayersActive;
	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad(gameObject);

		if (onlineGameInformations == null)
		{
			onlineGameInformations = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

}
