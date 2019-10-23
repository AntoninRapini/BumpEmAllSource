using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public static LevelManager levelManager;
	public GameObject[] SpawnPositions = new GameObject[4];

	void Awake()
	{
		if (levelManager == null)
		{
			levelManager = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

}
