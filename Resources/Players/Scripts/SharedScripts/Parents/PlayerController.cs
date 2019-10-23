using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Parent class for any player controllers , responsible for player movement
public class PlayerController : SimpleStateMachine
{
    // Movement
	protected float x, y;
	public float moveSpeed = 12, baseMoveSpeed = 12;
	protected Vector3 moveDirection;
	protected float moveAcceleration = 200;
	protected GameInputController playerInput;
	[HideInInspector]
    public Rigidbody playerRigidbody;



	[HideInInspector] public bool isCasting;
	[HideInInspector] public Animator animator;
    protected Player playerClass;
	[HideInInspector] public GameObject playerCursor;
	public enum PlayerStates {Idle, Move}

    public List<float> slowList = new List<float>();
	public bool lookAtCursor = true;
	public float baseColliderRadius;
	// Use this for initialization

	protected virtual void Start()
	{
		animator = transform.GetComponent<PlayerAnimations> ().animator;
		lookAtCursor = true;
		baseColliderRadius = transform.GetComponent<CapsuleCollider> ().radius;
		baseMoveSpeed = moveSpeed;
		playerClass = transform.GetComponent<Player> ();
		playerRigidbody = transform.GetComponent<Rigidbody>();
		currentState = PlayerStates.Idle;
		playerInput = transform.GetComponent<GameInputController> ();
	}

    protected virtual void ApplySlows()
    {
        for(int i = 0; i < slowList.Count; i++)
        {
            moveSpeed *= slowList[i];
        }
        
    }

	protected override void EarlyGlobalSuperUpdate()
	{
		if(playerInput.Current.PauseInput)
		{
			GameUIManager.gameUIManager.HandlePause ();
		}
		moveSpeed = baseMoveSpeed;
		ApplySlows();
	}


    protected virtual void Idle_EnterState()
    {
    }

	protected virtual  void Idle_Update()
    {
    }
		
	protected virtual void Move_Update()
    {
    }





	public virtual void Push(Vector3 spellPosition, float pushPower, GameObject source)
    {
		playerClass.killer = source;
        Vector3 pushDirection = transform.position - spellPosition;
        pushDirection.y = 0;
        pushDirection = pushDirection.normalized;
		float finalPushPower = pushPower;
		finalPushPower *=  (100-(playerClass.pushResistance*0.8f)) /100;
		playerRigidbody.AddForce(pushDirection * finalPushPower);
		playerClass.TakeDamage (0, gameObject);
	
    }


 }
