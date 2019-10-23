using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RockStanceQuit : MonoBehaviour {

	private List<GameObject> playerHit = new List<GameObject> ();
	
	void OnEnable()
	{
		playerHit.Clear ();
	}


	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player" && col.transform != transform.parent.transform.parent && !playerHit.Contains(col.gameObject))
		{
			playerHit.Add (col.gameObject);
			col.transform.GetComponent<PlayerController>().Push(transform.position, 1500, transform.parent.transform.parent.gameObject);
		}
	}

}
