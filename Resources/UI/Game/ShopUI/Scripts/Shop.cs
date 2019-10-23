using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class Shop : MonoBehaviour {

	private SpellIcon spellSelected;
	private bool playerReady;
	private bool spellAlreadyOwned;
	private bool hasStarted;
	private ShopNavigation shopNavigation;
	private ShopUI shopUI;
	private Spells sameSpell;

	[HideInInspector] public List<Spells> ownedSpells = new List<Spells>();
	[HideInInspector] public Player player;
	[HideInInspector] public SpellManager spellManager;

	void Start () 
	{
		shopUI = transform.GetComponent<ShopUI> ();
		shopNavigation = transform.GetComponent<ShopNavigation> ();
		shopNavigation.LoadVariables ();
		shopUI.LoadVariables ();
		SetPlayerNotReady();
		for(int i = 0; i < ownedSpells.Count; i++)
		{
			shopNavigation.spellChoiceContainer.transform.Find(ownedSpells[i].spellName).GetComponent<SpellIcon> ().ownedIcon.SetActive (true);
		}
		hasStarted = true;
	}


	void OnEnable()
	{
		if(hasStarted)
		{
			Select (shopNavigation.spellArray[0,0]);
			SetPlayerNotReady();
		}
	}


	public void Select(GameObject spell)
	{
		spellSelected = spell.GetComponent<SpellIcon>();
		shopUI.spellDescripton.GetComponent<Text> ().text = spellSelected.spell.spellDescription;
		shopUI.contourSpell.transform.GetComponent<RectTransform> ().localPosition = spell.transform.GetComponent<RectTransform> ().localPosition;
	}

	public void SetReadyStatus()
	{
		if(playerReady)
		{
			SetPlayerNotReady ();
		}
		else
		{
			SetPlayerReady ();
		}
	}

	public void SetPlayerReady()
	{
		playerReady = true;
		shopUI.readyCheck.SetActive (true);
		if(!GameManager.gameManager.playersReady.Contains(player))
		{
			GameManager.gameManager.playersReady.Add (player);
		}
		if(GameManager.gameManager.playersReady.Count == GameManager.gameManager.players.Length)
		{
			GameManager.gameManager.StartRound ();
		}
	}

	public void SetPlayerNotReady()
	{
		playerReady = false;
		shopUI.readyCheck.SetActive (false);
		if(GameManager.gameManager.playersReady.Contains(player))
		{
			GameManager.gameManager.playersReady.Remove (player);
		}
	}

	public bool AttemptSpellBuy()
	{
		spellAlreadyOwned = false;
		for(int i = 0; i < ownedSpells.Count; i++)
		{
			if(spellSelected.spell.spellName == ownedSpells[i].spellName)
			{
				sameSpell = ownedSpells [i];
				spellAlreadyOwned = true;
			}
		}

		if(spellAlreadyOwned)
		{
			shopUI.MessagePlayer("Reassign spell ?");
			return true;
		}
		else if (player.money >= spellSelected.spell.cost)
		{
			shopUI.MessagePlayer("Buy spell for " + spellSelected.spell.cost + " gold ?");
			return true;
		}

		return false;
	}

	public void HandleSpellAssignment(int selectedSpellSlot)
	{

		if (spellAlreadyOwned)
		{
			if (spellManager.ChosenSpells [selectedSpellSlot] != null)
			{
				ReplaceSpell (selectedSpellSlot);
			} 
			else
			{
				AssignSpellToSlot (selectedSpellSlot);
			}
		} 
		else
		{
			AssignNewSpell (selectedSpellSlot);
		}
	}


	public void ReplaceSpell(int selectedSpellSlot)
	{
		if (spellManager.ChosenSpells [selectedSpellSlot].spellName != spellSelected.spell.spellName)
		{
			bool hasFound = false;
			for (int i = 0; i < spellManager.ChosenSpells.Length; i++) 
			{
				if (spellManager.ChosenSpells [i] != null) 
				{
					if (sameSpell.spellName == spellManager.ChosenSpells [i].spellName)
					{
						Spells newSpell = spellManager.ChosenSpells [i];
						spellManager.ChosenSpells [i] = spellManager.ChosenSpells [selectedSpellSlot];
						spellManager.ChosenSpells [selectedSpellSlot] = newSpell;
						hasFound = true;
						break;
					}
				}
			}
			if (!hasFound) 
			{
				Destroy (spellManager.ChosenSpells [selectedSpellSlot]);
				Spells newSpell = player.gameObject.AddComponent (sameSpell.GetType ()) as Spells;
				spellManager.ChosenSpells [selectedSpellSlot] = newSpell;
			}
		} 
	}


	public void AssignSpellToSlot(int selectedSpellSlot)
	{
		bool hasFound = false;
		for (int i = 0; i < spellManager.ChosenSpells.Length; i++)
		{
			if (spellManager.ChosenSpells [i] != null) {
				
				if (sameSpell.spellName == spellManager.ChosenSpells [i].spellName) 
				{
					spellManager.ChosenSpells [selectedSpellSlot] = sameSpell;
					spellManager.ChosenSpells [i] = null;
					hasFound = true;
					break;
				}
			}
		}
		if (!hasFound)
		{
			Spells newSpell = player.gameObject.AddComponent (sameSpell.GetType ()) as Spells;
			spellManager.ChosenSpells [selectedSpellSlot] = newSpell;
		}
	}


	public void AssignNewSpell(int selectedSpellSlot)
	{
		if (spellManager.ChosenSpells [selectedSpellSlot] != null)
		{
			Destroy (spellManager.ChosenSpells [selectedSpellSlot]);
			spellManager.ChosenSpells [selectedSpellSlot] = null;
		}
		Spells newSpell = player.gameObject.AddComponent (spellSelected.spell.GetType ()) as Spells;
		newSpell.enabled = true;
		spellManager.ChosenSpells [selectedSpellSlot] = newSpell;
		player.money -= newSpell.cost;
		shopUI.UpdateMoney (player.money);
		ownedSpells.Add (newSpell);
		spellSelected.ownedIcon.SetActive (true);
	}
}
