using UnityEngine;
using System.Collections;

// base class for in game player input
public class GameInputController : MonoBehaviour {

	public PlayerInput Current = new PlayerInput();
    protected Player playerClass;
    protected int playerNumber;

	protected virtual void Start () 
	{
		playerClass = transform.GetComponent<Player>();
		playerNumber = playerClass.playerNumber;
	}

	void OnDisable()
	{
		Current = new PlayerInput()
		{
			PauseInput = false,
			MoveInput = Vector3.zero,
			CursorInput = Vector2.zero,
			SpellOne = false,
			SpellTwo = false,
			SpellThree = false,
			SpellFour = false,
			mouseClickInput = false
		
		};
	}

}



public struct PlayerInput
{
	public Vector3 MoveInput;
	public Vector2 CursorInput;
	public bool SpellOne;
	public bool SpellTwo;
	public bool SpellThree;
	public bool SpellFour;
	public bool mouseClickInput;
	public bool PauseInput;
}