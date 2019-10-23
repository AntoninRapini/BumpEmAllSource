using UnityEngine;
using System.Collections;

public class TourbillonEndFxMove : MonoBehaviour {


	public float moveSpeed = 5;
	float timeCounter = 0;
	public float distanceFromPlayer;
	// Use this for initialization

	void OnEnable()
	{
		timeCounter = 0;
	}
	// Update is called once per frame
	void Update () {

		timeCounter += Time.deltaTime*moveSpeed;
		float x = Mathf.Cos (timeCounter) * distanceFromPlayer;
		float z = Mathf.Sin (timeCounter) * distanceFromPlayer;

		transform.localPosition = new Vector3 (x, transform.localPosition.y, z);
	}
}
