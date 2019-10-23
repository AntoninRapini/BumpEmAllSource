using UnityEngine;
using System.Collections;

public class PlayerMenuInputController : MenuInputController {

    private PlayerSelectScript playerClass;
    private int playerNumber;
	private string horizontalMove, verticalMove, cursorHorizontal, cursorVertical, selectInput, deselectInput, pause;
	void Start () 
	{
		playerClass = transform.GetComponent<PlayerSelectScript> ();
		playerNumber = playerClass.playerNumber;
		horizontalMove = "Horizontal" + playerNumber;
		verticalMove = "Vertical" + playerNumber;
		cursorHorizontal = "CursorHorizontal"+ playerNumber;
		cursorVertical = "CursorVertical"+ playerNumber;
		selectInput = "Select"+ playerNumber;
		deselectInput = "Deselect"+ playerNumber;
		pause = "Start"+ playerNumber;

	

	}
	
	// Update is called once per frame
	void Update () {
    
		Vector2 rotateInput = new Vector2(Input.GetAxis(cursorHorizontal), Input.GetAxis(cursorVertical));

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
			



		bool select = Input.GetButtonDown (selectInput);
		bool deselect = Input.GetButtonDown (deselectInput);
		bool validate = Input.GetButtonDown (pause);

        Current = new MenuInputs()
        {
            Up = up,
            Down = down,
            Left = left,
            Right = right,
            Select = select,
            Deselect = deselect,
            Validate = validate,
            RotateInput = rotateInput
        };

	}

}


