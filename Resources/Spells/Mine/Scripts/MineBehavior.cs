using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineBehavior : SpellPrefabBehavior {


	private bool exploding;
	private List<GameObject> playerHit = new List<GameObject> ();
	public Material nonActivatedColor, activatedColor;
	private float activationTime = 1;
	private bool activated;
	private float explosionDuration = 0.5f , explosionStartTime;
	public Light mineLight;
	public GameObject explosionFX;
	public override void LoadVariables(Spells _spell, GameObject _spellCreator)
	{
		explosionFX = transform.Find ("Explosion").gameObject;
		mineLight = transform.Find ("FX/Light").GetComponent<Light> ();
		mineLight.color = Color.red;
		spell = _spell;
		spellOwner = _spellCreator;
		spellCreator = _spellCreator;
		transform.GetComponent<SpellInformations>().spellCreator = spellCreator;
		spellCreator = spell.transform.gameObject;
		timeCreated = Time.time;
		activated = false;
		exploding = false;
		playerHit.Clear();
		transform.GetComponent<SphereCollider> ().enabled = false;
		hasBeenInitialised = true;
	}

	void OnEnable()
	{
		if(hasBeenInitialised)
		{
			Reset ();
		}
	}

	public void Reset()
	{
		playerHit.Clear();
		mineLight.color = Color.red;
		activated = false;
		exploding = false;
		timeCreated = Time.time;
		transform.GetComponent<SphereCollider> ().enabled = false;
	}

	void Update()
	{
		if(Time.time > timeCreated + activationTime && !activated)
		{
			mineLight.color = Color.green;
			activated = true;
		}
		if(exploding && Time.time > explosionStartTime + explosionDuration)
		{
			Die();
		}
	}


	public override void OnTriggerEnter(Collider col)
	{
		if(activated && !exploding)
		{
			if(col.tag == "Player")
			{
				explosionStartTime = Time.time;
				Explode();

			}
		}
		else if(activated && exploding)
		{
			if (col.tag == "Player" && !playerHit.Contains (col.gameObject)) 
			{
				col.transform.GetComponent<PlayerController>().Push(transform.position, spell.pushPower, spellCreator);
				col.transform.GetComponent<Player>().TakeDamage(spell.damage, spellCreator);
				playerHit.Add (col.gameObject);
			}
		}
	}

	void Explode()
	{
		exploding = true;
		transform.GetComponent<SphereCollider> ().enabled = true;
		explosionFX.SetActive (true);

	}
	public override void Die()
	{
		spellCreator = spellOwner;
		explosionFX.SetActive (false);
		transform.gameObject.SetActive(false);
	}

}
