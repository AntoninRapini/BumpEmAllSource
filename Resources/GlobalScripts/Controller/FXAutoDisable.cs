using UnityEngine;
using System.Collections;

public class FXAutoDisable : MonoBehaviour {

	public float timeAlive;
	private float timeCreated;

	void OnEnable()
	{
		timeCreated = Time.time;
	}

	void Update()
	{
		if(Time.time > timeCreated + timeAlive)
		{
			gameObject.SetActive (false);
		}
	}
}
