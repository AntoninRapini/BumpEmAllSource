using UnityEngine;
using System.Collections;

public class ProjectileBehavior : SpellPrefabBehavior {

	public Vector3 targetPosition;
	protected Vector3 direction;
	protected float projectileSpeed;


	public override void LoadVariables(Spells _spell, GameObject _spellCreator)
	{
		base.LoadVariables (_spell, _spellCreator);
		projectileSpeed = spell.travelSpeed;
		GetDirection ();
	}

	public virtual void GetDirection()
	{
		direction = transform.SetNormalizedDirection (spell.Cursor);
		if(direction ==  Vector3.zero)
		{
			direction = spellCreator.transform.forward;
			direction = direction.normalized;
		}
		direction.y = 0;
	
	}

	public override void ReflectShieldHit (Transform shield)
	{
		if(shield.parent.GetComponent<PlayerFX>() != null)
		{
			shield.parent.GetComponent<PlayerFX> ().PlayFX ("ShieldHit");
		}

		spellCreator = shield.parent.gameObject;
		direction = shield.transform.SetNormalizedDirection (transform);
		direction.y = 0;
		timeCreated = Time.time;
	}

	public override void MeleeAttackHit (Transform meleeAttack)
	{
		ReflectShieldHit (meleeAttack);
	}


}
