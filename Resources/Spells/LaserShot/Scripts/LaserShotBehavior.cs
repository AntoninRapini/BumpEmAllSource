using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaserShotBehavior : SpellPrefabBehavior {

	private List<GameObject> playersHit = new List<GameObject> ();
	private float currentZScale;
	private float lerpTimer;


	public override void LoadVariables (Spells _spell, GameObject _spellCreator)
	{
		base.LoadVariables (_spell, _spellCreator);
		transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, 0.1f);
		lerpTimer = 0;
	}

	void OnEnable()
	{
		if(hasBeenInitialised)
		{
			transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, 0.1f);
			lerpTimer = 0;
			timeCreated = Time.time;
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
		if(col.CompareTag("Player") && col.gameObject != spellCreator && !playersHit.Contains(col.gameObject))
		{
			PlayerHit (col.transform);
		}

	}

	public override void PlayerHit(Transform playerHit)
	{
		playersHit.Add (playerHit.gameObject);
		playerHit.GetComponent<Player>().TakeDamage(spell.damage, spellCreator);
		playerHit.GetComponent<PlayerFX> ().PlayFX ("Hit");;
	}

	public override void Die()
	{
		spellCreator = spellOwner;
		playersHit.Clear ();
		transform.gameObject.SetActive(false);
	}
}
