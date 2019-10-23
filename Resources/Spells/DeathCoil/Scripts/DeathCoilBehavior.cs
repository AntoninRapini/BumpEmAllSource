using UnityEngine;
using System.Collections;

public class DeathCoilBehavior : ProjectileBehavior {

	public GameObject target;

	public override void LoadVariables(Spells _spell, GameObject _spellCreator)
	{
		spell = _spell;
		spellOwner = _spellCreator;
		spellCreator = _spellCreator;
		transform.GetComponent<SpellInformations>().spellCreator = spellCreator;
		spellCreator = spell.transform.gameObject;
		projectileSpeed = spell.travelSpeed;
		timeCreated = Time.time;
		target = FindTarget();
		hasBeenInitialised = true;
	}

	void OnEnable()
	{
		if(hasBeenInitialised)
		{
			timeCreated = Time.time;
			target = FindTarget();
			if(target == gameObject)
			{
				Die ();
			}
		}
	}

	void Update()
	{
		if(target.activeSelf)
		{
			if(target != null)
			{
				transform.position = Vector3.MoveTowards (transform.position , target.transform.position, projectileSpeed * Time.deltaTime);
			}
			else
			{
				target = FindTarget();
				if(target == gameObject || target == null)
				{
					Die ();
				}
			}
		}
		else
		{
			target = FindTarget();
			if(target == gameObject || target == null)
			{
				Die ();
			}
		}
	}


	public override void PlayerHit (Transform player)
	{
		player.GetComponent<PlayerFX> ().PlayFX ("DeathCoilHit");
		player.GetComponent<Player>().TakeDamage(spell.damage, spellCreator);
		Die ();
	}

	public override void ReflectShieldHit (Transform reflector)
	{
		reflector.parent.GetComponent<PlayerFX> ().PlayFX ("ShieldHit");
		target = spellCreator;
		spellCreator = reflector.parent.gameObject;
	}


	public GameObject FindTarget()
	{
		return gameObject.GetClosestGameObjectInList (GameManager.gameManager.alivePlayers, spellCreator);
	}

}
