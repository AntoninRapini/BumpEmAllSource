using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonBreathBehavior : SpellPrefabBehavior {

	private Vector3 Direction;
	private float ProjectileSpeed;
	private float currentZScale;
	private List<GameObject> playersHit = new List<GameObject> ();
	bool hasStarted;
	private float lerpTimer;

	public override void LoadVariables (Spells _spell, GameObject _spellCreator)
	{
		base.LoadVariables (_spell, _spellCreator);
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, 0.1f);
		lerpTimer = 0;
		transform.SetParent (_spellCreator.transform);
	}


	void OnEnable()
	{
		
		if(hasBeenInitialised)
		{
			timeCreated = Time.time;
			transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, 0.1f);
			lerpTimer = 0;
		}
	}

	void Update()
	{
		currentZScale = Mathf.Lerp (0.1f, 1, lerpTimer / lifeTime);
		lerpTimer += Time.deltaTime;
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, currentZScale);
		if(Time.time > timeCreated + lifeTime)
		{
			Die();
		}
	}


	public override void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player" && col.gameObject != spellCreator && !playersHit.Contains(col.gameObject))
		{
			PlayerHit (col.transform);
		}

		else  if(col.tag == "ReflectShield" && col.transform.parent.gameObject != spellCreator) 
		{
			col.transform.parent.GetComponent<PlayerFX> ().PlayFX ("ShieldHit");
			Die ();
		}
	}

	public override void PlayerHit (Transform playerHit)
	{
		BreakSpellCast (playerHit);
		ApplyStun (playerHit);
		playerHit.GetComponent<Player>().TakeDamage(spell.damage, spellCreator);
		playersHit.Add (playerHit.gameObject);

	}

	void ApplyStun(Transform playerHit)
	{
		Stun playerHitStun = playerHit.GetComponent<Stun> ();

		if (playerHitStun != null)
		{
			playerHitStun.Refresh();
			playerHitStun.duration = 2;
		}
		else
		{
			Stun stun = playerHit.gameObject.AddComponent<Stun>();
			stun.duration = 2;
		}
	}


	public override void Die()
	{
		spellCreator = spellOwner;
		playersHit.Clear ();
		transform.gameObject.SetActive(false);
	}
}
