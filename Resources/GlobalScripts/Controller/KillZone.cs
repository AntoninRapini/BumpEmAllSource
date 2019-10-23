using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player")
		{
			col.transform.GetComponent<Player> ().Die ();
		}
	}
}
