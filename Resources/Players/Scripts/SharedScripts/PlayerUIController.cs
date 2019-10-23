using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

	private SpellManager spellManager;
	public PlayerUI playerUI;
    bool hasStarted;
	public Player player;
	public Sprite noSpell;
	public SpellUI[] spellsUIs = new SpellUI[4];
	// Use this for initialization
	void Start () {
		spellManager = transform.GetComponent<SpellManager> ();
		playerUI.gameObject.SetActive (true);
		spellsUIs = playerUI.spellUIs;
		noSpell = Resources.Load<Sprite>("UI/Game/PlayerUI/Sprites/Spells/NoSpell");

		for(int i = 0; i < spellsUIs.Length; i++)
		{
			spellsUIs [i].cooldownBar.fillAmount = 0;
			spellsUIs [i].globalCooldownBar.fillAmount = 0;
		}
		UpdateRoundWon ();
		UpdateSpellIcons ();
        hasStarted = true;
	}

    void OnEnable()
    {
        if(hasStarted)
        {
			UpdateRoundWon ();
            UpdateMoney();
            UpdateHealth();
            UpdateSpellIcons();
        }
      
    }


	public void UpdateSpellIcons()
	{
		for(int i = 0; i < spellsUIs.Length; i++)
		{
			if(spellManager.ChosenSpells[i] != null)
			{
				spellsUIs [i].spellImage.sprite = Resources.Load<Sprite> (spellManager.ChosenSpells [i].iconPath);
			
			}
            else
            {
				spellsUIs[i].spellImage.sprite = noSpell;
            }


        }
	}

	void Update()
	{

		{
			// PUT SILENCE ICON
		}
		for(int i = 0; i < spellsUIs.Length; i++)
		{
			if(spellManager.ChosenSpells[i] != null && spellManager.ChosenSpells[i].cooldown != 0 && spellManager.ChosenSpells[i].lastSpellCastTime != 0)
			{
				spellsUIs [i].cooldownBar.fillAmount = ((spellManager.ChosenSpells[i].lastSpellCastTime + spellManager.ChosenSpells[i].cooldown) - Time.time )/ spellManager.ChosenSpells[i].cooldown;
			}
			else
			{
				spellsUIs [i].cooldownBar.fillAmount = 0;
			}
		}
			
		if(spellManager.lastSpellCast != 0)
		{
			for(int i = 0; i < spellManager.ChosenSpells.Length; i++)
			{
				if(spellManager.ChosenSpells[i] != null)
				{
					if(spellManager.ChosenSpells[i].InGCD)
					{
						spellsUIs [i].globalCooldownBar.fillAmount = ((spellManager.globalCooldown + spellManager.lastSpellCast) - Time.time )/ spellManager.globalCooldown;
					}
					else
					{
						spellsUIs [i].globalCooldownBar.fillAmount = 0;
					}
				}
			}
		}
			

	}


	public void UpdateHealth()
	{
		playerUI.resistance.text = player.pushResistance.ToString();
	}

	public void UpdateMoney()
	{
		playerUI.money.text = player.money.ToString();
	}
	
	public void UpdateRoundWon()
	{
		playerUI.roundWon.text = player.roundWon.ToString();
	}
}
