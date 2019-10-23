using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController_PC : PlayerController
{
    // Movement

	private UnityEngine.AI.NavMeshAgent navAgent;
	private Vector3 direction;

	protected override void Start () 
	{
		base.Start ();
		navAgent = transform.GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}

	void OnEnable()
	{
		direction = Vector3.zero;
	}

 

    protected override void LateGlobalSuperUpdate()
    {

		if (HasDirection())
		{
			playerRigidbody.MovePosition (transform.position + moveDirection * Time.deltaTime);
		}

		animator.SetFloat("speed", moveDirection.magnitude);
    
		if (Input.GetMouseButtonDown(1))
		{
			direction = SetDirection ();
		}

    }

	protected override void ApplySlows()
    {
        for(int i = 0; i < slowList.Count; i++)
        {
            moveSpeed *= slowList[i];
        }
        
    }


	protected override void Idle_EnterState()
    {
        moveDirection = Vector3.zero;
    }

	protected override void Idle_Update()
    {

		if (HasDirection())
        {
            currentState = PlayerStates.Move;
        }

        
    }

	/*public Vector3 GetMousePosition()
	{
		Vector3 screentoworldpointPosition;
		Vector3 targetPosition;
		screentoworldpointPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,	Input.mousePosition.y + Camera.main.transform.position.y	, Input.mousePosition.y));
		targetPosition.x = screentoworldpointPosition.x;
		targetPosition.z = screentoworldpointPosition.z;
		targetPosition.y = 0;
		return targetPosition;
	}*/


	public Vector3 SetDirection()
	{
		return new Vector3(playerCursor.transform.position.x, transform.position.y, playerCursor.transform.position.z);
	}

	protected override void Move_Update()
    {
       
		if(HasDirection())
		{
			if(lookAtCursor)
			{
				transform.rotation = Quaternion.LookRotation (direction - transform.position);
				transform.rotation = Quaternion.Euler (0, transform.rotation.eulerAngles.y, 0);
			}
			moveDirection = Vector3.MoveTowards(moveDirection, Movement() * moveSpeed, moveAcceleration * Time.deltaTime);
		}
        else
        {
			currentState = PlayerStates.Idle;
        }
    }

	private bool HasDirection()
	{
		if(transform.position == direction || direction == Vector3.zero)
		{
			return false;
		}
		return true;
	}


	private Vector3 Movement()
	{
		Vector3 currentMovement = new Vector3();
		currentMovement += direction - transform.position;
		currentMovement.y = 0;
		if(currentMovement.magnitude < 0.2f)
		{
			direction = Vector3.zero;
			return Vector3.zero;
		}
		return currentMovement.normalized;
	}





 }
