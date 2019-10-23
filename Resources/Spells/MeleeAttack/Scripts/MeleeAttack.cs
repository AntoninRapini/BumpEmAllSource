using UnityEngine;
using System.Collections;

public class MeleeAttack : TriggerZoneSpells, Icancelable {


	public float slowPercentage = 0.5f;
	public float timeStarted;
	public float lastTickTime, timeBetweenTicks = 1;
	public bool hasStopped;
	public int currentPushPower, currentDamage;
	public MeleeAttack()
	{
		// Animations
		animationName = "MeleeCharge";
		animationState = 3;

		// Balance
		InGCD = true;
		cooldown = 0;
		pushPower = 400;
		damage = 6;
		cancellable = true;
		cost = 350;
		projectileLifeTime = 0.7f;

		// Cast Conditions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());

		// Infos
		SpellBehavior = "MeleeAttackBehavior";
		spellDescription = "Attack in a cone in front of you, can be charged to deal more damage and push foes harder, can be used to throw projectiles back !";
		spellName = "MeleeAttack";
		prefabName = "MeleeAttackPrefab";
		prefabPath = "Spells/MeleeAttack/Prefab/MeleeAttackPrefab";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();
	

	}


	public override void SpellCast()
	{


		if(!isBeingChanneled)
		{
			StartCharging ();
		}
		else if(isBeingChanneled && Time.time > timeStarted + 0.1f)
		{
			Hit ();
		}



	}


	void BreakChannel()
	{

		playerFXs.StopFX ("MeleeCharge");

        hasStopped = false;
        isBeingChanneled = false;
		// A test en multi
		playerAnimationManager.ChangeCastingStatus (false);
		playerAnimationManager.ChangeAnimationState ("Idle");
		playerController.slowList.Remove(slowPercentage);
		spellManager.silenced = false;
		StopCoroutine ("StopAnimation");
		if(prefab != null)
		{
			prefab.SetActive (false);
		}

	}

	void StartCharging()
	{
		playerFXs.PlayFX ("MeleeCharge");
        lastTickTime = Time.time;
		currentPushPower = pushPower;
		currentDamage = damage;
		isBeingChanneled = true;
		playerController.slowList.Add(slowPercentage);
		timeStarted = Time.time;
		PlayAnimation ();
		playerAnimationManager.ChangeCastingStatus (true);
		spellManager.silenced = true;
	}


	void Hit()
	{
		playerFXs.StopFX ("MeleeCharge");
        isBeingChanneled = false;
		playerController.slowList.Remove(slowPercentage);
		playerAnimationManager.ChangeCastingStatus (false);
		SpawnPrefab ();
		prefab.transform.SetParent (transform);
		prefab.transform.localPosition = GetSpawnLocation ();
		StartGCD ();
		spellManager.silenced = false;
        hasStopped = false;
	}




	public override Vector3 GetSpawnLocation()
	{
		return new Vector3(0 , 0.63f, 0.56f);
	}

	public override Quaternion GetSpawnRotation ()
	{
		return transform.rotation;
	}
		
	void UpdateDamage()
	{
		currentPushPower += 400;
		currentDamage += 4;
		currentPushPower = Mathf.Clamp (pushPower, 400, 1200);
		currentDamage = Mathf.Clamp (damage, 4, 20);
	}


	void Update()
	{	
		
		if(isBeingChanneled)
		{
			if(Time.time > lastTickTime + timeBetweenTicks)
			{
				UpdateDamage ();
				lastTickTime = Time.time;
			}	
		}
	}

	public override void Cancel()
	{
		BreakChannel();
	}


}
