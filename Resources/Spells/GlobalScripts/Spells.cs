using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Spells : MonoBehaviour
{

    public string SpellBehavior;



	public string animationName;
	public int animationState;


	// Spell Infos
	public string spellDescription;
	public string spellName;
	public string prefabName;
	public GameObject spellPrefab;
	public Sprite icon;
	public string iconPath;
	public int spellNumber;
	public bool cancellable;
	public int cost;
	public bool isBeingChanneled;
	public float projectileLifeTime;

	public List<CastCondition> castConditions = new List<CastCondition>();

	// Spell Stats
    public int damage;
    public int pushPower;
	public int range{get ; set ;}
	public int cooldown;
	public int travelSpeed;


    public string prefabPath;
    public bool InGCD;
    public float lastSpellCastTime;
    public Transform Cursor;


	public PlayerAnimations playerAnimationManager;
	public PlayerController playerController;
	public Player player;
	public SpellManager spellManager;

	public bool hasBeenInitialised;
	public PlayerFX playerFXs;
	public virtual void SpellCast()
	{
	}
		
	public virtual void Start()
	{
		playerFXs = transform.GetComponent<PlayerFX> ();
		icon = Resources.Load<Sprite>(iconPath);
		spellPrefab = Resources.Load(prefabPath) as GameObject;
		playerAnimationManager = transform.GetComponent<PlayerAnimations> ();
		playerController = transform.GetComponent<PlayerController> ();
		player = transform.GetComponent<Player> ();
		spellManager =  transform.GetComponent<SpellManager> ();
		Cursor = player.playerCursor.transform;
		hasBeenInitialised = true;
	}
		
    public virtual void Cancel()
    {
    }

	public bool CanCastSpell()
	{
		for(int i = 0; i < castConditions.Count; i++)
		{
			if(!castConditions[i].ConditionMet(this))
			{
				return false;
			}
		}
		return true;
	}

	public void StartGCD()
	{
		spellManager.lastSpellCast = Time.time;
	}

	public virtual void PlayAnimation()
	{
		playerAnimationManager.ChangeAnimationState (animationName);
	}
}


interface Icancelable
{
    void Cancel();
}


