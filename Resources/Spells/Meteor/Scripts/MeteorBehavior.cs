using UnityEngine;
using System.Collections;

public class MeteorBehavior : ProjectileBehavior {

	private GameObject Particles, MeteorGroundHitFX;

	private CapsuleCollider capsuleCollider;
	private bool isDead;




	public override void LoadVariables(Spells _spell, GameObject _spellCreator)
	{
		spell = _spell;
		spellOwner = _spellCreator;
		spellCreator = _spellCreator;
		transform.GetComponent<SpellInformations>().spellCreator = spellCreator;
		spellCreator = spell.transform.gameObject;


		isDead = false;
		transform.GetComponent<MeshRenderer>().enabled = true;
		capsuleCollider = transform.GetComponent<CapsuleCollider> ();
		Particles =	transform.Find ("Particles").gameObject;
		MeteorGroundHitFX = transform.Find ("MeteorGroundHit").gameObject;
		capsuleCollider.enabled = true;
		Particles.SetActive (true);
		MeteorGroundHitFX.SetActive (false);

		projectileSpeed = spell.travelSpeed;
		lifeTime = spell.projectileLifeTime;
		timeCreated = Time.time;
		GetDirection ();
		hasBeenInitialised = true;
	}

	void OnEnable()
	{

		if(hasBeenInitialised)
		{
			isDead = false;
			transform.GetComponent<MeshRenderer>().enabled = true;
			capsuleCollider.enabled = true;
			Particles.SetActive (true);
			MeteorGroundHitFX.SetActive (false);
			timeCreated = Time.time;
			GetDirection ();
		
		}


	}


	void Update()
	{
		if(!isDead)
		{
			gameObject.transform.Translate(direction * projectileSpeed * Time.deltaTime);
		}
	
	}
		
	IEnumerator DieCoroutine()
	{
		isDead = true;
		transform.GetComponent<MeshRenderer>().enabled = false;
		yield return new WaitForSeconds (3);
		Die ();
	}
		

	public override void DecorHit (Transform decor)
	{
		capsuleCollider.enabled = false;
		Particles.SetActive (false);
		MeteorGroundHitFX.SetActive (true);
		StartCoroutine ("DieCoroutine");
	}

	public override void PlayerHit (Transform player)
	{
		player.GetComponent<PlayerController>().Push(transform.position, spell.pushPower, spellCreator);
		player.GetComponent<Player>().TakeDamage(spell.damage, spellCreator);
		player.GetComponent<PlayerFX> ().PlayFX ("Hit");

	}

	public override void ReflectShieldHit(Transform shield)
	{
	}

	public override void MeleeAttackHit (Transform meleeAttack)
	{
		direction =  transform.position - meleeAttack.parent.transform.position;
		direction = direction.normalized;
		direction.y = 0;
		projectileSpeed *= 10;
	}
		
	public override void GetDirection()
	{
		direction = spell.Cursor.transform.GetComponent<CursorController> ().floorPointHit - transform.position;
		direction.x = 0;
		direction.z = 0;
		direction = direction.normalized;
	}
}
