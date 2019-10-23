using UnityEngine;
using System.Collections;

public class Charge : Spells, Icancelable {


	public float slowPercentage = 0;
	public Vector3 chargeDirection;
	public bool charging;
	public float castTime;
	public float maxDuration = 0.4f;
	public float speed = 0.6f;
	public Charge()
	{
		// Animations
		animationName = "ShieldOn";
		animationState = 5;

		// Balance
		cost = 500;
		InGCD = true;
		damage = 5;
		pushPower = 1200;
		cooldown = 12;


		// Cast Condtions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());
		castConditions.Add(new NotInCooldown());


		// Infos
		SpellBehavior = "Charge";
		spellDescription = "Charge towards your cursor, stopping after any collision or after a certain distance, bumps ennemies hit";
		spellName = "Charge";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();
	
	
	}

	public override void SpellCast ()
	{
		startCharge ();
	}



	void startCharge()
	{
		playerFXs.PlayFX ("ChargeFX");
		PlayAnimation ();
		playerAnimationManager.ChangeCastingStatus (true);
		playerController.lookAtCursor = false;
		castTime = Time.time;
		StartGCD ();
		chargeDirection = Cursor.position - transform.position;
		chargeDirection = chargeDirection.normalized * speed;
		playerController.slowList.Add (slowPercentage);
		spellManager.silenced = true;
		charging = true;
	}

	void stopCharge()
	{
		playerFXs.StopFX ("ChargeFX");
		playerAnimationManager.ChangeCastingStatus (false);
		playerController.lookAtCursor = true;
		charging = false;
		lastSpellCastTime = Time.time;
		playerController.slowList.Remove (slowPercentage);
		playerController.isCasting = false;
		spellManager.silenced = false;
	}

	public override void Cancel()
	{
		stopCharge ();
	}

	void FixedUpdate()
	{
		if(charging)
		{
			if(Time.time > castTime + maxDuration)
			{
				stopCharge ();
			}
			playerController.playerRigidbody.MovePosition (transform.position + chargeDirection);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(charging)
		{
			if(col.transform.tag == "Player")
			{
				col.transform.GetComponent<PlayerController>().Push (transform.position, pushPower, gameObject);
				col.transform.GetComponent<Player> ().TakeDamage (damage, gameObject);
				col.transform.Find ("FX/Hit").gameObject.SetActive (true);
			}

			if (col.transform.tag != "Floor")
			{
				stopCharge ();
			}
		}
	
	
	}
}
