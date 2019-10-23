using UnityEngine;
using System.Collections;


// Input controller for players in the couch party mode
public class GameInputController_Local : GameInputController {


	private bool spellOneIsInUse;
	private bool spellTwoIsInUse;
	private bool pauseIsInUse;
	private string horizontalMove, verticalMove, cursorHorizontal, cursorVertical, spellOne, spellTwo, spellThree, spellFour, pause;
	// Use this for initialization

	protected override void Start () {
		base.Start ();
		horizontalMove = "Horizontal" + playerNumber;
		verticalMove = "Vertical" + playerNumber;
		cursorHorizontal = "CursorHorizontal" + playerNumber;
		cursorVertical = "CursorVertical" + playerNumber;
		spellOne = "SpellOne" + playerNumber;
		spellTwo = "SpellTwo" + playerNumber;
		spellThree = "SpellThree" + playerNumber;
		spellFour = "SpellFour" + playerNumber;
		pause = "Start" + playerNumber;
	}


	void Update () 
	{
		Vector3 moveInput = new Vector3(Input.GetAxisRaw(horizontalMove), 0, Input.GetAxisRaw(verticalMove));
		Vector2 cursorInput = new Vector2(Input.GetAxis(cursorHorizontal), Input.GetAxis(cursorVertical));

		bool spellOnePressed = false;

		if (Input.GetAxisRaw(spellOne) > 0)
		{

			if (!spellOneIsInUse)
			{

				spellOnePressed = true;
				spellOneIsInUse = true;
			}
		}
		else
		{
			spellOneIsInUse = false;
		}


		bool spellTwoPressed = false;
		if (Input.GetAxisRaw(spellTwo) < 0)
		{
			if (!spellTwoIsInUse)
			{

				spellTwoPressed = true;
				spellTwoIsInUse = true;
			}
		}
		else
		{
			spellTwoIsInUse = false;

		}

		bool pauseInput = false;
		if(Input.GetButton(pause))
		{
			if(!pauseIsInUse)
			{
				pauseInput = true;
				pauseIsInUse = true;
			}
			
		}
		else
		{
			pauseIsInUse = false;
		}
	


		bool spellThreePressed = Input.GetButtonDown(spellThree);
		bool spellFourPressed = Input.GetButtonDown(spellFour);



		Current = new PlayerInput()
		{
			PauseInput = pauseInput,
			MoveInput = moveInput,
			CursorInput = cursorInput,
			SpellOne = spellOnePressed,
			SpellTwo = spellTwoPressed,
			SpellThree = spellThreePressed,
			SpellFour = spellFourPressed
		};
	}
}

