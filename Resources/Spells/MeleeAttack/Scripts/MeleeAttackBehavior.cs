using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttackBehavior : SpellPrefabBehavior {

	private List<GameObject> playerHit = new List<GameObject> ();


	void OnEnable()
	{
		if(hasBeenInitialised)
		{
			timeCreated = Time.time;
		}
	}

	void Update()
	{

		if(Time.time > timeCreated + lifeTime)
		{
			Die();
		}
	}


	public override void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player" && col.gameObject != spellCreator && !playerHit.Contains(col.gameObject))
		{
			playerHit.Add (col.gameObject);
			col.transform.GetComponent<PlayerController>().Push(transform.position, spell.pushPower, spellCreator);
			col.transform.GetComponent<Player>().TakeDamage(spell.damage, spellCreator);
			col.transform.GetComponent<PlayerFX> ().PlayFX ("Hit");
		
		}
		else if(col.tag == "ReflectShield"  && !playerHit.Contains(col.gameObject))
		{
			playerHit.Add (col.transform.parent.gameObject);
			spellCreator.GetComponent<PlayerController>().Push(transform.position, spell.pushPower, spellCreator);
			spellCreator.GetComponent<Player>().TakeDamage(spell.damage, spellCreator);
			col.transform.parent.GetComponent<PlayerFX> ().PlayFX ("ShieldHit");
		}

	}


	public override void Die()
	{
		spellCreator = spellOwner;
		playerHit.Clear ();
		transform.gameObject.SetActive(false);
	}
}
