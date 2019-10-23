using UnityEngine;
using System.Collections;

public class CursorController_PC : CursorController {


	private PlayerController playerController;


	void Start () 
	{
		playerController = player.transform.GetComponent<PlayerController> ();
		input = player.transform.GetComponent<GameInputController> ();
		oldPosition = player.transform.position;
		currentY = transform.position.y;
		transform.GetComponent<MeshRenderer> ().material = Resources.Load ("UI/Game/PlayerUI/Materials/Cursors/Cursor" + player.GetComponent<Player> ().playerNumber) as Material;	
		transform.position = player.position;
	}

	void Update () 
	{

		if (Physics.Raycast(transform.position + new Vector3(0, 5, 0), -Vector3.up, out rayHit, 20, myLayerMask))
		{
			if(!rayHit.collider.isTrigger)
			{
				floorPointHit = rayHit.point;
				currentY = floorPointHit.y + 0.5f;
			}
		}
		newPosition = player.transform.position - oldPosition;
		oldPosition = player.transform.position;

		if (input.Current.CursorInput != Vector2.zero)
		{
			nextPosition = transform.position + new Vector3(input.Current.CursorInput.x * Time.deltaTime * cursorSpeed, 0, -input.Current.CursorInput.y * Time.deltaTime * cursorSpeed);
			if (CanMove(nextPosition))
			{
				transform.position = nextPosition;
			}
			else
			{
				/*transform.position = nextPosition;
				Vector3 allowedPosition = transform.position - player.position;
				transform.position = player.position + Vector3.ClampMagnitude (allowedPosition, 30);*/
			}
				
			transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
		

		}
	}


	bool CanMove(Vector3 nextPosition)
	{
		Vector3 positionOnScreen = Camera.main.WorldToScreenPoint (nextPosition);
		int height = Screen.currentResolution.height;
		int width = Screen.currentResolution.width;
		if(positionOnScreen.x > width || positionOnScreen.x < 0 || positionOnScreen.y > height || positionOnScreen.y < 0)
		{
			return false;
		}
		return true;
	}

}


