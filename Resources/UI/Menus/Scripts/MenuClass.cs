using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// Base class for any menu controlled by a controller, first item in array must be the highest on screen
public class MenuClass : MonoBehaviour {

	protected int placeInButtonArray;
	public Button[] buttons;
	protected MenuInputController input;
	protected float lastInputTime = 0;
	protected float minTimeBetweenInputs = 0.2f;
	protected bool hasStarted;
	public bool outOfTime;

	protected void Start()
	{
		placeInButtonArray = 0; 
		buttons [placeInButtonArray].Select ();
		hasStarted = true;
		input = transform.GetComponent<MenuInputController> ();
	}


	protected void OnEnable()
	{
		if(hasStarted)
		{
			placeInButtonArray = 0; 
			buttons [placeInButtonArray].Select ();
		}
	}

	protected virtual void Update () 
	{
		if(input.Current.Up)
		{
			DownArray();
		}
		else if(input.Current.Down)
		{
			UpArray ();
		}
		if(input.Current.Select)
		{
			Submit ();
		}
	}

	protected void UpArray()
	{
		if (Time.time > lastInputTime + minTimeBetweenInputs || !outOfTime) 
		{
			if(placeInButtonArray ==  buttons.Length - 1)
			{
				placeInButtonArray = 0;
				buttons [placeInButtonArray].Select();

			}
			else
			{
				placeInButtonArray++;
				buttons [placeInButtonArray].Select();
			}
			lastInputTime = Time.time;
		}

	
	}

	protected void DownArray()
	{
		if (Time.time > lastInputTime + minTimeBetweenInputs || !outOfTime)
		{
			if (placeInButtonArray == 0) 
			{
				placeInButtonArray = buttons.Length - 1;
				buttons [placeInButtonArray].Select ();
			}
			else 
			{
				placeInButtonArray--;
				buttons [placeInButtonArray].Select ();
			}
			lastInputTime = Time.time;
		}
	}

	protected void Submit()
	{
		buttons [placeInButtonArray].onClick.Invoke();
	}

	public void SelectButton(Button button)
	{
		for(int i = 0; i < buttons.Length; i++)
		{
			if(buttons[i] == button)
			{
				placeInButtonArray = i;
			}
		}
	
		button.Select ();
	}
}
