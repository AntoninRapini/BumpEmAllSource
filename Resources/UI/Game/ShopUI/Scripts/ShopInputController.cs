using UnityEngine;
using System.Collections;

public class ShopInputController : MenuInputController {

	private Player playerClass;
	private int playerNumber;
	private bool spellOneIsInUse, spellTwoIsInUse;
	private string horizontalMove, verticalMove, cursorHorizontal, cursorVertical, spellOne, spellTwo, spellThree, spellFour, select, deselect, start;
	[HideInInspector] public bool navigationInput;

	void Start () {
		playerClass = transform.GetComponent<Shop> ().player;
		//playerNumber = playerClass.playerNumber;
		playerNumber = 1;

		horizontalMove = "Horizontal" + playerNumber;
		verticalMove = "Vertical" + playerNumber;
		cursorHorizontal = "CursorHorizontal" + playerNumber;
		cursorVertical = "CursorVertical" + playerNumber;
		spellOne = "SpellOne" + playerNumber;
		spellTwo = "SpellTwo" + playerNumber;
		spellThree = "SpellThree" + playerNumber;
		spellFour = "SpellFour" + playerNumber;
		select = "Select" + playerNumber;
		deselect = "Deselect" + playerNumber;
		start = "Start" + playerNumber;
	}

	// Update is called once per frame
	void Update () {

		navigationInput = false;
		bool up = false;

		if (Input.GetAxisRaw(verticalMove) < 0)
		{

			if (!upIsBeingPressed)
			{

				up = true;
				upIsBeingPressed = true;
			}
		}
		else
		{
			upIsBeingPressed = false;
		}

		bool down = false;

		if (Input.GetAxisRaw(verticalMove) > 0)
		{

			if (!downIsBeingPressed)
			{

				down = true;
				downIsBeingPressed = true;
			}
		}
		else
		{
			downIsBeingPressed = false;
		}


		bool left = false;
		if (Input.GetAxisRaw(horizontalMove) < 0)
		{

			if (!leftIsBeingPressed)
			{

				left = true;
				leftIsBeingPressed = true;
			}
		}
		else
		{
			leftIsBeingPressed = false;
		}

		bool right = false;
		if (Input.GetAxisRaw(horizontalMove) > 0)
		{
			if (!rightIsBeingPressed)
			{

				right = true;
				rightIsBeingPressed = true;
			}
		}
		else
		{
			rightIsBeingPressed = false;
		}




		bool selectPressed = Input.GetButtonDown (select);
		bool deselectPressed = Input.GetButtonDown (deselect);
		bool validatePressed = Input.GetButtonDown (start);


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



		bool spellThreePressed = Input.GetButtonDown(spellThree);
		bool spellFourPressed = Input.GetButtonDown(spellFour);

		if(up	|| down || right || left)
		{
			navigationInput = true;
		}
		Current = new MenuInputs()
		{
			Up = up,
			Down = down,
			Left = left,
			Right = right,
			Select = selectPressed,
			Deselect = deselectPressed,
			Validate = validatePressed,
			SpellOne = spellOnePressed,
			SpellTwo = spellTwoPressed,
			SpellThree = spellThreePressed,
			SpellFour = spellFourPressed
		};

	}
}

