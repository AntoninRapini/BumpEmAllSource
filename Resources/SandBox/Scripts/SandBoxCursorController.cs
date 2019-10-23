using UnityEngine;
using System.Collections;

public class SandBoxCursorController : CursorController {


	public float controllerCursorSpeed;
	public float mouseCursorSpeed;
	[HideInInspector]

	private GameInputController_Local controllerInput;
	private GameInputController_PC pcInput;

	private SandBoxPlayerController playerController;

	void Start () 
	{
		playerController = player.transform.GetComponent<SandBoxPlayerController> ();
		controllerInput = player.transform.GetComponent<GameInputController_Local> ();
		pcInput = player.transform.GetComponent<GameInputController_PC> ();
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

		if(!playerController.PCInputs)
		{
			if (controllerInput.Current.CursorInput != Vector2.zero)
			{
				nextPosition = transform.position + new Vector3(controllerInput.Current.CursorInput.x * Time.deltaTime * controllerCursorSpeed, 0, -controllerInput.Current.CursorInput.y * Time.deltaTime * controllerCursorSpeed);
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
		else
		{
			if (pcInput.Current.CursorInput != Vector2.zero)
			{
				nextPosition = transform.position + new Vector3(pcInput.Current.CursorInput.x * Time.deltaTime * mouseCursorSpeed, 0, -pcInput.Current.CursorInput.y * Time.deltaTime * mouseCursorSpeed);
				if (Vector3.Distance(nextPosition, new Vector3(player.transform.position.x, transform.position.y,player.transform.position.z)) > 30)
				{
					transform.position = nextPosition;
					Vector3 allowedPosition = transform.position - player.position;
					transform.position = player.position + Vector3.ClampMagnitude (allowedPosition, 30);
				}
				else
				{
					transform.position = nextPosition;
				}
					
				transform.position = new Vector3(transform.position.x, currentY, transform.position.z);

			}
			else
			{
				if (Vector3.Distance(transform.position, new Vector3(player.transform.position.x, transform.position.y,player.transform.position.z)) > 30)
				{
					Vector3 allowedPosition = transform.position - player.position;
					transform.position = player.position + Vector3.ClampMagnitude (allowedPosition, 30);
				}

			}
		}
	
	}
}
