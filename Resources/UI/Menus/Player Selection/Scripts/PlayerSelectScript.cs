using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerSelectScript : SimpleStateMachine {

 

	// UI
	public TextMesh[] skinParts = new TextMesh[3];
	private int currentPlaceInSkinParts = 0;
	private float lastInputTime, minTimeBetweenInputs = 0.2f;
	public GameObject activeScreen, inactiveScreen;
    public TextMesh readyText;

    // Skins
    private SkinManager_Local skinManager;

    // Logic
	private bool hasStarted;
    private MenuInputController input;
    private bool active;
    private enum playerSelectStates { Inactive, Active, Ready };

    // Player
    public Transform playerModel;
    public int playerNumber;
    [HideInInspector] public PlayerInstance_Local playerInstance;

    void LoadVariables()
    {
        skinManager = transform.GetComponent<SkinManager_Local>();
        playerInstance.playerNumber = playerNumber;
        readyText.text = "Not Ready";
        readyText.color = Color.red;
        input = transform.GetComponent<MenuInputController>();
        currentPlaceInSkinParts = 0;
        hasStarted = true;
    }

	void OnEnable()
	{
		if(hasStarted)
		{
			readyText.text = "Not Ready";
			readyText.color = Color.red;
			SetState ();
		}
	}
		
	void Inactive_EnterState()
	{
		activeScreen.SetActive (false);
		inactiveScreen.SetActive (true);
        skinManager.RemoveCurrentSkins();
		skinManager.SetSkinsToDefault ();
	}

	void Inactive_LateUpdate()
	{
		if (Time.time > lastInputTime + minTimeBetweenInputs) 
		{
			if (input.Current.Select) 
			{
				active = true;
				GameInformations.gameInformations.numberOfPlayersActive++;
				currentState = playerSelectStates.Active;
				lastInputTime = Time.time;
			}
		}
	}

	void Inactive_ExitState()
	{
		inactiveScreen.SetActive (false);
		skinManager.skinTypeSelected = skinParts [currentPlaceInSkinParts].transform;
		Focus (skinParts[1],skinParts [currentPlaceInSkinParts]);
	}

	void Active_EnterState()
	{
		inactiveScreen.SetActive (false);
		activeScreen.SetActive (true);
	}
		

	void Active_LateUpdate()
	{

		if(Time.time > lastInputTime + minTimeBetweenInputs)
		{
			if(input.Current.Up)
			{
				lastInputTime = Time.time;
				Up ();
			}
			else if(input.Current.Down)
			{
				lastInputTime = Time.time;
				Down();
			}
			else if(input.Current.Left)
			{
				lastInputTime = Time.time;
				skinManager.ChangeSkin (-1);
			}
			else if(input.Current.Right)
			{
				lastInputTime = Time.time;
                skinManager.ChangeSkin (1);
			}

			if(input.Current.Deselect)
			{
				active = false;
				GameInformations.gameInformations.numberOfPlayersActive--;
				lastInputTime = Time.time;
				currentState = playerSelectStates.Inactive;
			}

			if(input.Current.Validate)
			{
				lastInputTime = Time.time;
				currentState = playerSelectStates.Ready;
			}
		}

		if(input.Current.RotateInput != Vector2.zero)
		{
			playerModel.transform.Rotate (0, -input.Current.RotateInput.x*2,0 );
		}


	}


	void Ready_EnterState()
	{
        skinManager.SaveSkins(ref playerInstance);
		GameInformations.gameInformations.AddPlayer (playerInstance);
		readyText.text = "Ready !";
		readyText.color = Color.green;
	}

	void Ready_LateUpdate()
	{
		if (input.Current.Validate || input.Current.Deselect) 
		{
			currentState = playerSelectStates.Active;
		}
	}

	void Ready_ExitState()
	{
		GameInformations.gameInformations.RemovePlayer (playerInstance);
		readyText.text = "Not Ready";
		readyText.color = Color.red;
	}
		
	void Up()
	{
		TextMesh old = skinParts [currentPlaceInSkinParts];
		if(currentPlaceInSkinParts == 0)
		{
			currentPlaceInSkinParts = skinParts.Length - 1;
		}
		else
		{
			currentPlaceInSkinParts--;
		}
		Focus (old,  skinParts [currentPlaceInSkinParts]);
	}

	void Down()
	{
		TextMesh old = skinParts [currentPlaceInSkinParts];
		if(currentPlaceInSkinParts == skinParts.Length - 1)
		{
			currentPlaceInSkinParts = 0;
		}
		else
		{
			currentPlaceInSkinParts++;

		}
		Focus (old,  skinParts [currentPlaceInSkinParts]);
	}

	
	void Focus(TextMesh old, TextMesh selected)
	{
		selected.color = Color.cyan;
		old.color = Color.yellow;
		skinManager.skinTypeSelected = skinParts [currentPlaceInSkinParts].transform;
	}
   
	void SetState()
	{
		if(active)
		{
			currentState = playerSelectStates.Active;
		}
		else
		{
			currentState = playerSelectStates.Inactive;
		}
	}

    public void RetrieveInstance(PlayerInstance_Local instance)
    {
        LoadVariables();
        playerInstance = instance;
        skinManager.RetrieveSkins(playerInstance);
        active = true;
		Debug.Log ("retrieve instance");
        SetState();
    }

    public void LoadDefault()
    {
        LoadVariables();
        skinManager.SetSkinsToDefault();
        SetState();
    }
}
