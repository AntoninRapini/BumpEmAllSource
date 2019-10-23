using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


// Handles shop UI
public class ShopUI : MonoBehaviour {

	// UI Elements
	public GameObject spellDescripton;
	public List<GameObject>	chosenSpellsUI = new List<GameObject> ();
	public GameObject contourSpell;
	public Text numberOfRoundsWon;
	public GameObject textPrompt;
	public Text moneyHolder;
	public GameObject readyCheck;
	public Text textPromptText;

	// Check if variables were already loaded
	bool variablesLoaded;

	private ShopNavigation shopNavigation;
	private Shop shop;



	void OnEnable()
	{
		if(variablesLoaded)
		{
			readyCheck.SetActive (false);
			UpdateMoney (shop.player.money);
			UpdateSpellIcons ();
			numberOfRoundsWon.text = "Round won : " + shop.player.roundWon.ToString();
		}
	}
		

	public void UpdateMoney(int money)
	{
		moneyHolder.text  = money.ToString();
	}
		
	public void UpdateSpellIcons()
	{
		for(int i = 0; i < shop.spellManager.ChosenSpells.Length; i++)
		{
			if(shop.spellManager.ChosenSpells[i] != null)
			{
				chosenSpellsUI [i].transform.Find ("SpellImage").GetComponent<Image> ().sprite = Resources.Load<Sprite> (shop.spellManager.ChosenSpells [i].iconPath);
			}
			else
			{
				GameObject container = Resources.Load("UI/Game/PlayerUI/Prefabs/Spells/NoSpell") as GameObject;
				chosenSpellsUI[i].transform.Find("SpellImage").GetComponent<Image>().sprite = container.GetComponent<SpriteRenderer>().sprite;
			}
		}
	}


	public void MessagePlayer(string message)
	{
		textPrompt.SetActive (true);
		textPromptText.text = message;
	}

	public void RemoveMessageBox()
	{
		textPrompt.SetActive (false);
	}
		
	public void LoadVariables()
	{
		/*numberOfRoundsWon = transform.FindChild("roundWon").GetComponent<Text>();
		textPrompt = transform.FindChild ("FondTexte").gameObject;
		spellDescripton = transform.FindChild ("DescriptionText").gameObject;
		contourSpell = shopNavigation.spellChoiceContainer.transform.FindChild ("ContourSpell").gameObject;
		moneyHolder = transform.FindChild ("Text").FindChild ("PlayerGold").transform.GetComponent<Text> ();
		readyCheck = transform.FindChild ("ReadyCheck").gameObject;*/

		shopNavigation = transform.GetComponent<ShopNavigation> ();
		shop = transform.GetComponent<Shop> ();
		readyCheck.SetActive(false);
		textPrompt.SetActive (false);
		numberOfRoundsWon.text = "Round won : " + shop.player.roundWon.ToString();
		UpdateMoney (shop.player.money);
		UpdateSpellIcons ();
		variablesLoaded = true;
	}
}
