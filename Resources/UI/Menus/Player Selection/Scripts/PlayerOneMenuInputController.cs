using UnityEngine;
using System.Collections;

public class PlayerOneMenuInputController : MenuInputController {



	void LateUpdate () {
       
		bool up = false;

		if (Input.GetAxisRaw("Vertical1") < 0 || (Input.GetAxisRaw("Vertical") < 0))
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

		if (Input.GetAxisRaw("Vertical1" ) > 0 || (Input.GetAxisRaw("Vertical") > 0))
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
		if (Input.GetAxisRaw("Horizontal1" ) < 0 || (Input.GetAxisRaw("Horizontal") < 0))
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
		if (Input.GetAxisRaw("Horizontal1" ) > 0 || (Input.GetAxisRaw("Horizontal") > 0))
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
			

		bool select = Input.GetButtonDown ("Select1" );
		bool deselect = Input.GetButtonDown ("Deselect1");
		bool validate = Input.GetButtonDown ("Start1" );
 
		Current = new MenuInputs()
        {
			Up = up,
			Down = down,
			Left = left,
			Right = right,
			Select = select,
			Deselect = deselect,
			Validate = validate
        };

	}
}

