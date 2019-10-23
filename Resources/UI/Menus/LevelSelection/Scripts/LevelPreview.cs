using UnityEngine;
using System.Collections;


// Controls the 3D Model of the level in the level select screen
public class LevelPreview : MonoBehaviour {

	bool hasStarted;
	private Vector3 startRotation;

	void Start () {
		startRotation = transform.localRotation.eulerAngles;
		hasStarted = true;
	}

	void OnEnable()
	{
		if(hasStarted)
		{
			transform.localRotation = Quaternion.Euler(startRotation);
		}
	}


	void Update () {
	

		if(Input.GetAxis("CursorHorizontal1") != 0)
		{
			transform.Rotate (0, Input.GetAxisRaw("CursorHorizontal1")* -2 , 0);
		}
	}
}
