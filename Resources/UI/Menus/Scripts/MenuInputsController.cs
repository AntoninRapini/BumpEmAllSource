using UnityEngine;
using System.Collections;

// Base input controller class for any menu
public class MenuInputController : MonoBehaviour {

	public MenuInputs Current;
	protected bool leftIsBeingPressed, rightIsBeingPressed, downIsBeingPressed, upIsBeingPressed;
	// Use this for initialization
	void Start () {

		Current = new MenuInputs();
	
	}

	void OnDisable()
	{
		Current = new MenuInputs()
		{
			Up = false,
			Down = false,
			Left = false,
			Right = false,
			Deselect = false,
			Validate = false,
			SpellOne = false,
			SpellTwo = false,
			SpellThree = false,
			SpellFour = false,
			RotateInput = Vector2.zero
		};
	}

}

public struct MenuInputs
{

	public bool Up;
	public bool Down;
	public bool Left;
	public bool Right;
	public bool Select;
	public bool Deselect;
	public bool Validate;
	public Vector2 RotateInput;
	public bool SpellOne;
	public bool SpellTwo;
	public bool SpellThree;
	public bool SpellFour;


}