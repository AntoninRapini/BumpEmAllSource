using UnityEngine;
using System.Collections;

public class SandBoxPlayerController : PlayerController {

	// Movement

	private GameInputController_Local controllerInput;
	private GameInputController_PC pcInput;
	public bool PCInputs;

	protected override void Start()
	{
		playerClass = transform.GetComponent<Player> ();
		animator = transform.GetComponent<PlayerAnimations> ().animator;
		lookAtCursor = true;
		baseColliderRadius = transform.GetComponent<CapsuleCollider> ().radius;
		baseMoveSpeed = moveSpeed;         
		playerRigidbody = transform.GetComponent<Rigidbody>();
		controllerInput = gameObject.GetComponent<GameInputController_Local>();
		pcInput = GetComponent<GameInputController_PC> ();
		currentState = PlayerStates.Idle;
	}


	protected override void EarlyGlobalSuperUpdate()
	{
		moveSpeed = baseMoveSpeed;
		ApplySlows();
	}

	protected override void LateGlobalSuperUpdate()
	{
		playerRigidbody.MovePosition (transform.position + moveDirection * Time.deltaTime);
		if(lookAtCursor)
		{
			transform.rotation = Quaternion.LookRotation (new Vector3 (playerCursor.transform.position.x, transform.position.y, playerCursor.transform.position.z) - transform.position);
		}
		animator.SetFloat("speed", moveDirection.magnitude);


	}
		
	protected override void Idle_EnterState()
	{
		moveDirection = Vector3.zero;
	}

	protected override void Idle_Update()
	{
		if (controllerInput.Current.MoveInput != Vector3.zero)
		{
			currentState = PlayerStates.Move;
		}
	}

	public Vector3 GetMousePosition()
	{
		Vector3 screentoworldpointPosition;
		Vector3 targetPosition;
		screentoworldpointPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,	Input.mousePosition.y + Camera.main.transform.position.y	, Input.mousePosition.y));
		targetPosition.x = screentoworldpointPosition.x;
		targetPosition.z = screentoworldpointPosition.z;
		targetPosition.y = 0;
		return targetPosition;
	}

	protected override void Move_Update()
	{
		if (controllerInput.Current.MoveInput != Vector3.zero)
		{
			moveDirection = Vector3.MoveTowards(moveDirection, Movement() * moveSpeed, moveAcceleration * Time.deltaTime);
		}
		else
		{
			currentState = PlayerStates.Idle;
		}
	}


	private Vector3 Movement()
	{
		Vector3 currentMovement = new Vector3();
		currentMovement += Vector3.right * controllerInput.Current.MoveInput.x;
		currentMovement -= Vector3.forward * controllerInput.Current.MoveInput.z;
		return currentMovement.normalized;
	}
		


}
