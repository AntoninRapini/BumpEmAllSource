using UnityEngine;
using System.Collections;


// input controller for players in online game
public class GameInputController_PC : GameInputController {

    private bool spellOneIsInUse;
    private bool spellTwoIsInUse;


	
	// Update is called once per frame
	void Update () 
	{
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		Vector2 cursorInput = new Vector2(Input.GetAxis("CursorHorizontal"), -Input.GetAxis("CursorVertical"));

		bool pauseInput = Input.GetButton ("Cancel");
		bool spellOne = Input.GetButtonDown("SpellOne");
		bool spellTwo = Input.GetButtonDown("SpellTwo");
        bool spellThree = Input.GetButtonDown("SpellThree");
        bool spellFour = Input.GetButtonDown("SpellFour");



		Current = new PlayerInput()
        {
			PauseInput = pauseInput,
            MoveInput = moveInput,
            CursorInput = cursorInput,
            SpellOne = spellOne,
            SpellTwo = spellTwo,
            SpellThree = spellThree,
            SpellFour = spellFour
        };

	}
}


