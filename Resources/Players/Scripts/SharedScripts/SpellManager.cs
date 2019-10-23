using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

public class SpellManager : MonoBehaviour {

	[HideInInspector] public Spells[] ChosenSpells = new Spells[5];
	[HideInInspector] public float globalCooldown = 1, lastSpellCast;
	[HideInInspector] public GameObject cursor;
	[HideInInspector] public bool silenced;

    private GameInputController input;
	private int spellCastSlot;

	void Start () 
	{
        input = transform.GetComponent<GameInputController>();
        lastSpellCast = 0;
		spellCastSlot = -1;
    }

	public void LoadFirstSpells ()
	{
		ChosenSpells [0] = gameObject.AddComponent<FireBolt> ();
		ChosenSpells [1] = gameObject.AddComponent<IceShot> ();
		ChosenSpells [1] = gameObject.AddComponent<DragonBreath> ();
		for(int i = 0; i < ChosenSpells.Length; i++)
		{
			if(ChosenSpells[i] != null)
			{
				ChosenSpells [i].lastSpellCastTime = 0;
			}
		}
	}



	void Update ()
	{	
		spellCastSlot = GetInput ();

		// Check If input was pressed
		if(spellCastSlot != -1)
		{
				// Check if there is a spell bound to the input
				if (ChosenSpells[spellCastSlot] != null) 
				{
					if(ChosenSpells[spellCastSlot].CanCastSpell())
					{
						ChosenSpells [spellCastSlot].SpellCast ();
					}
				}
		}         
 }
	
	
	public bool PlayerInGlobalCooldown()
	{
		if(lastSpellCast != 0)
		{
			return Time.time < lastSpellCast + globalCooldown;
		}
		else
		{
			return false;
		}

	}

	private void CastSpell(int spellSlot)
	{
		ChosenSpells [spellSlot].SpellCast ();
	}


	public void ResetSpells()
	{
		for(int i = 0; i < ChosenSpells.Length; i++)
        {
            if(ChosenSpells[i] != null)
            {
				if(ChosenSpells[i].hasBeenInitialised)
				{
					ChosenSpells[i].Cancel();
				}
				ChosenSpells[i].lastSpellCastTime = 0;
            }
        }
		lastSpellCast = 0;
	}

	public int GetInput()
	{
		if(input.Current.SpellOne)
		{
			return 0;
		}
		else if(input.Current.SpellTwo)
		{
			return 1;
		}
		else if(input.Current.SpellThree)
		{
			return 2;
		}
		else if(input.Current.SpellFour)
		{
			return 3;
		}
		return -1;
	}
    

}

