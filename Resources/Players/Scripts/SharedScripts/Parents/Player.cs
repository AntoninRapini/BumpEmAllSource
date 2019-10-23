using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {



	// Player variables
	public int money;
	public int roundWon;
	public int pushResistance;
	private int startPushResistance = 100;
	public int goldGained = 0;

	public GameObject killer;
	public Dictionary<string, Debuff>  debuffDictionary = new Dictionary<string, Debuff>();
	[HideInInspector] public GameObject playerCursor;
	protected PlayerUIController playerUIController;
	protected Vector3 spawnLocation;
	[HideInInspector] public PlayerInstance_Local playerInstance;
	// Player infos
	[HideInInspector] public int playerNumber;


	protected Dictionary<string, Debuff> debuffGarbageCollector = new Dictionary<string, Debuff>();
	protected PlayerController playerController;
	protected SpellManager spellManager;

	protected bool hasStarted;

	public virtual void LoadVariables()
	{	
		playerController = transform.GetComponent<PlayerController> ();
		pushResistance = startPushResistance;
		spellManager = transform.GetComponent<SpellManager> ();
		spellManager.LoadFirstSpells();
		LoadGameUI ();
	}

	protected void Start()
	{
		Reset ();
		Spawn ();
		hasStarted = true;
	}
		
	protected void OnEnable()
	{
		if(hasStarted)
		{
			Reset ();
			Spawn ();
		}
			
	}


	protected virtual void LoadGameUI()
	{
		playerUIController = transform.GetComponent<PlayerUIController>();
		playerUIController.playerUI = GameUIManager.gameUIManager.gameUI.transform.GetComponent<GameUIElements> ().playerUIs [playerNumber - 1].transform.GetComponent<PlayerUI>();
		playerUIController.player = this;
		playerUIController.UpdateMoney ();
	}


    public void TakeDamage(int damage, GameObject source)
    {
		foreach (KeyValuePair<string, Debuff> item in debuffDictionary) 
		{
			if(item.Value is IBreakable)
			{
				debuffGarbageCollector.Add (item.Key, item.Value);
			}
		}
			
		foreach (KeyValuePair<string, Debuff> item in debuffGarbageCollector) 
		{
			item.Value.Break ();
		}

		if(damage > 0)
		{
			killer = source;
		}

		debuffGarbageCollector.Clear();
	
		if(pushResistance - damage < 0)
		{
			pushResistance = 0;
		}
		else
		{
			pushResistance -= damage;
		}
		playerUIController.UpdateHealth ();
    }

	public virtual void LoadShop(Shop shop)
	{
		Debug.Log ("shop fail load");
	}

	public virtual void Reset()
	{
		transform.position = spawnLocation;
		transform.rotation = Quaternion.identity;
		playerController.animator.gameObject.transform.localRotation = new Quaternion(0,0,0,0);
		pushResistance = startPushResistance;
		transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
		playerUIController.UpdateHealth ();
		playerController.animator.Rebind ();
		spellManager.ResetSpells ();
		RemoveDebuffs ();
	}

	public virtual void Die()
	{
	}

	public virtual void Spawn()
	{
    }
		

	public void RemoveDebuffs()
	{
		foreach (KeyValuePair<string, Debuff> item in debuffDictionary) 
		{
			item.Value.Break ();
		}
	}


	public virtual void GainMoney(int _money)
	{
	}
  
}
