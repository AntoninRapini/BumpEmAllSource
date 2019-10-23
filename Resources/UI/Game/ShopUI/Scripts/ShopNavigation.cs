using UnityEngine;
using System.Collections;


// Handles input in the shop and communicates them to the ShopUI and Shop scripts

public class ShopNavigation : SimpleStateMachine {

	private Shop shop;
	private ShopUI shopUI;
	public enum arrayDirections { Up, Down, Left, Right};
	private ShopInputController input;
	private float lastInputTime, minTimeBetweenInputs = 0.2f;
	public GameObject spellChoiceContainer;

	// Spell Icons and navigation through them
	public GameObject[,] spellArray = new GameObject[3,5];
	private int spellArrayXMax = 2, spellArrayYMax = 4;
	private int currentX, currentY; 

	public enum ShopStates {Navigating, ConfirmingSpellBuy, AssigningSpellInput}

	bool hasStarted;



	void OnEnable()
	{
		if(hasStarted)
		{
			lastInputTime = Time.time;
			currentX = 0;
			currentY = 0;
			shop.Select (spellArray[currentX,currentY]);

			currentState = ShopStates.Navigating;
		}

	}

	void Navigating_Update()
	{

		if(input.Current.Select)
		{
			shop.SetPlayerNotReady ();
			if(shop.AttemptSpellBuy())
			{
				currentState = ShopStates.ConfirmingSpellBuy;
			}
		}

		else if(input.Current.Deselect)
		{
			shop.SetPlayerNotReady ();
		}

		if(input.navigationInput && Time.time > lastInputTime + minTimeBetweenInputs)
		{
			ApplyNavigationInput ();
		}

		if(input.Current.Validate)
		{
			shop.SetReadyStatus ();
		}

	}


	void ConfirmingSpellBuy_Update()
	{
		if(input.Current.Select)
		{
			currentState = ShopStates.AssigningSpellInput;
		}
		if(input.Current.Deselect)
		{
			shopUI.RemoveMessageBox ();
			currentState = ShopStates.Navigating;
		}
	}

	void AssigningSpellInput_EnterState()
	{
		shopUI.MessagePlayer ("Press the button you wish to bind the spell to (LT, RT, LB, RB)");
	}

	void AssigningSpellInput_Update()
	{
			
		int selectedSpellSlot = -1;
		if (input.Current.SpellOne)
		{
			selectedSpellSlot = 0;
		} 
		else if (input.Current.SpellTwo)
		{
			selectedSpellSlot = 1;

		}
		else if (input.Current.SpellThree)
		{
			selectedSpellSlot = 2;

		} 
		else if (input.Current.SpellFour) 
		{
			selectedSpellSlot = 3;

		}
		else if (input.Current.Deselect) 
		{
			shopUI.RemoveMessageBox ();
			currentState = ShopStates.Navigating;
			return;
		}
		if(selectedSpellSlot != -1)
		{
			shop.HandleSpellAssignment (selectedSpellSlot);
			shopUI.RemoveMessageBox ();
			shopUI.UpdateSpellIcons ();
			currentState = ShopStates.Navigating;
		}
	
		
		
	}



	public void FillSpellIconsArray()
	{
		spellArray [0, 0] = spellChoiceContainer.transform.Find ("FireBolt").gameObject;
		spellArray [0, 1] = spellChoiceContainer.transform.Find ("Meteor").gameObject;
		spellArray [0, 2] = spellChoiceContainer.transform.Find ("IceShot").gameObject;
		spellArray [0, 3] = spellChoiceContainer.transform.Find ("DeathCoil").gameObject;
		spellArray [0, 4] = spellChoiceContainer.transform.Find ("LaserShot").gameObject;
		spellArray [1, 0] = spellChoiceContainer.transform.Find ("MeleeAttack").gameObject;
		spellArray [1, 1] = spellChoiceContainer.transform.Find ("Charge").gameObject;;
		spellArray [1, 2] = spellChoiceContainer.transform.Find ("Tourbillon").gameObject;
		spellArray [1, 3] = spellChoiceContainer.transform.Find ("Mine").gameObject;
		spellArray [1, 4] = spellChoiceContainer.transform.Find ("DragonBreath").gameObject;
		spellArray [2, 0] = null;
		spellArray [2, 1] = spellChoiceContainer.transform.Find ("PhaseShift").gameObject;
		spellArray [2, 2] = spellChoiceContainer.transform.Find ("ReflectShield").gameObject;
		spellArray [2, 3] = spellChoiceContainer.transform.Find ("RockStance").gameObject;
		spellArray [2, 4] = spellChoiceContainer.transform.Find ("Blink").gameObject;
	}




	void ApplyNavigationInput()
	{
		if (input.Current.Up)
		{
			MoveInArray (arrayDirections.Up);
		} 
		else if (input.Current.Down) 
		{
			MoveInArray (arrayDirections.Down);
		}
		else if (input.Current.Left) 
		{
			MoveInArray (arrayDirections.Left);
		} 
		else if (input.Current.Right) 
		{
			MoveInArray (arrayDirections.Right);
		}

		lastInputTime = Time.time;
	}

	void MoveInArray(arrayDirections arrayDirection)
	{
		switch(arrayDirection)
		{
		case arrayDirections.Up:
			Up ();
			break;
		case arrayDirections.Down:
			Down ();
			break;
		case arrayDirections.Left:
			Left ();
			break;
		case arrayDirections.Right:
			Right ();
			break;
		default:
			break;
		}
	}

	void Up()
	{
		currentX = (currentX == 0) ? spellArrayXMax : currentX - 1;

		if(spellArray[currentX, currentY] != null)
		{
			shop.Select (spellArray [currentX, currentY]);
		}
		else
		{
			Up ();
		}

	}

	void Left()
	{

		currentY = (currentY == 0) ? spellArrayYMax : currentY - 1;

		if(spellArray[currentX, currentY] != null)
		{
			shop.Select (spellArray [currentX, currentY]);
		}
		else
		{
			Left ();
		}
	}

	void Right()
	{
		currentY = (currentY == spellArrayYMax) ? 0 : currentY + 1;

		if(spellArray[currentX, currentY] != null)
		{
			shop.Select (spellArray [currentX, currentY]);
		}
		else
		{
			Right ();
		}
	}

	void Down()
	{
		currentX = (currentX == spellArrayXMax) ? 0 : currentX + 1;

		if(spellArray[currentX, currentY] != null)
		{
			shop.Select (spellArray [currentX, currentY]);
		}
		else
		{
			Down ();
		}
	}


	public void LoadVariables()
	{
		shop = transform.GetComponent<Shop> ();
		shopUI = transform.GetComponent<ShopUI> ();
		input = transform.GetComponent<ShopInputController> ();
		lastInputTime = Time.time;
		currentX = 0;
		currentY = 0;
		FillSpellIconsArray ();
		shop.Select (spellArray[currentX,currentY]);
		currentState = ShopStates.Navigating;
		hasStarted = true;
	}


}
