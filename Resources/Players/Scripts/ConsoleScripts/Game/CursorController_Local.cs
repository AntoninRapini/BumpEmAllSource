using UnityEngine;
using System.Collections;

public class CursorController_Local : CursorController {


	void Start () 
	{
		input = player.transform.GetComponent<GameInputController> ();
		oldPosition = player.transform.position;
		currentY = transform.position.y;
		transform.GetComponent<MeshRenderer> ().material = Resources.Load ("UI/Game/PlayerUI/Materials/Cursors/Cursor" + player.GetComponent<Player> ().playerNumber) as Material;	
	}
	

	void Update () 
	{
        if (Physics.Raycast(transform.position + new Vector3(0, 5, 0), -Vector3.up, out rayHit, 20, myLayerMask))
        {
			if(!rayHit.collider.isTrigger)
			{
				floorPointHit = rayHit.point;
				currentY = rayHit.point.y + 0.5f;
			}
	
        }
        newPosition = player.transform.position - oldPosition;
        oldPosition = player.transform.position;

       	 if (input.Current.CursorInput != Vector2.zero)
	     {
			nextPosition = transform.position + new Vector3(input.Current.CursorInput.x * Time.deltaTime * cursorSpeed, 0, -input.Current.CursorInput.y * Time.deltaTime * cursorSpeed);
			if (Vector3.Distance(nextPosition, new Vector3(player.transform.position.x, transform.position.y,player.transform.position.z)) > 10)
			{
				 transform.position = nextPosition;
				 Vector3 allowedPosition = transform.position - player.position;
				 transform.position = player.position + Vector3.ClampMagnitude (allowedPosition, 10);
			}
			else
			{
			 	transform.position = nextPosition;
			}
				
			transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
		
		}
		else
		{
			if (Vector3.Distance(transform.position, new Vector3(player.transform.position.x, transform.position.y,player.transform.position.z)) > 10)
			{
				Vector3 allowedPosition = transform.position - player.position;
				transform.position = player.position + Vector3.ClampMagnitude (allowedPosition, 10);
			}
		
		}
	}
}
