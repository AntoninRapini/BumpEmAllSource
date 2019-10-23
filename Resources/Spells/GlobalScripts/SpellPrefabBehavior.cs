using UnityEngine;
using System.Collections;

public class SpellPrefabBehavior : MonoBehaviour {

	protected GameObject spellOwner;
	public GameObject spellCreator;
	public Spells spell;
	protected float timeCreated;
	protected float lifeTime;
	protected bool hasBeenInitialised;

	public virtual void LoadVariables(Spells _spell, GameObject _spellCreator)
	{
		spell = _spell;
		spellOwner = _spellCreator;
		spellCreator = _spellCreator;
		transform.GetComponent<SpellInformations>().spellCreator = spellCreator;
		spellCreator = spell.transform.gameObject;
		lifeTime = spell.projectileLifeTime;
		timeCreated = Time.time;
		hasBeenInitialised = true;
	}
		
		
	public virtual void PlayerHit(Transform player)
	{
	}

	public virtual void ReflectShieldHit(Transform shield)
	{
	}

	public virtual void OtherSpellHit(Transform spell)
	{
		Die ();
	}

	public virtual void MeleeAttackHit(Transform meleeAttack)
	{
	}

	public virtual void DecorHit(Transform decor)
	{
		Die ();
	}

	public virtual void Die()
	{
		spellCreator = spellOwner;
		transform.gameObject.SetActive(false);
	}
		

	public virtual void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player") && col.gameObject != spellCreator)
		{
			PlayerHit (col.transform);
		}
		else if(col.CompareTag("Spell"))
		{
			OtherSpellHit (col.transform);
		}
		else  if (col.CompareTag("ReflectShield") && col.transform.parent.gameObject != spellCreator)
		{
			ReflectShieldHit (col.transform);
		}
		else  if(col.CompareTag("MeleeAttack"))
		{
			MeleeAttackHit (col.transform);
		}
		else if(col.CompareTag("Decor") || col.CompareTag("KillZone"))
		{
			DecorHit (col.transform);
		}

		Debug.Log (col.transform.name);
	}

	protected void BreakSpellCast(Transform playerHit)
	{
		playerHit.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		Spells[] playerHitSpells = playerHit.gameObject.GetComponent<SpellManager> ().ChosenSpells;
		for (int i = 0; i < playerHitSpells.Length; i++)
		{
			if (playerHitSpells[i] != null && playerHitSpells[i] is Icancelable)
			{
				playerHitSpells[i].Cancel();
			}
		}
	}
		
}
