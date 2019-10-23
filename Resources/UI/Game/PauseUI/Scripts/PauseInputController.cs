using UnityEngine;
using System.Collections;

public class PauseInputController : MenuInputController {


	void Update () {
		bool up = false;

		if (Input.GetAxisRaw("Vertical1") < 0 || Input.GetAxisRaw("Vertical2") < 0 || Input.GetAxisRaw("Vertical3") < 0 || Input.GetAxisRaw("Vertical4") < 0)
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

		if (Input.GetAxisRaw("Vertical1") > 0 || Input.GetAxisRaw("Vertical2") > 0 || Input.GetAxisRaw("Vertical3") > 0 || Input.GetAxisRaw("Vertical4") > 0)
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



		bool select = Input.GetButtonDown ("Select1")  || Input.GetButtonDown("Select2") || Input.GetButtonDown("Select3") || Input.GetButtonDown("Select4");
		bool validate = Input.GetButtonDown ("Start1") ||  Input.GetButtonDown ("Start2") ||  Input.GetButtonDown ("Start3") ||  Input.GetButtonDown ("Start4");
		bool deselect = Input.GetButtonDown ("Deselect1")  || Input.GetButtonDown("Deselect2") || Input.GetButtonDown("Deselect3") || Input.GetButtonDown("Deselect4");
		Current = new MenuInputs()
		{
			Deselect = deselect,
			Up = up,
			Down = down,
			Select = select,
			Validate = validate,
		};

	}
}