using UnityEngine;
using System.Collections;

public class LaserShot : TriggerZoneSpells {

	public float slowPercentage = 0;
	public float timeStarted;
	public float channelDuration = 2;
	public bool hasStopped;

	public LaserShot()
	{

		// Animations
		animationName = "ShieldOn";
		animationState = 5;

		// Balance
		InGCD = true;
		travelSpeed = 25;
		damage = 40;
		cooldown = 10;
		cost = 1250;
		projectileLifeTime = 1;

		// Cast Conditions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());
		castConditions.Add(new NotInCooldown());

		// Infos
		SpellBehavior = "LaserShot";
		spellDescription = "After 2 seconds of channeling, cast a huge laser in direction of your cursor, dealing massive damage to any enemy hit";
		spellName = "LaserShot";
		prefabName = "LaserShotPrefab";
		prefabPath = "Spells/LaserShot/Prefab/LaserShotPrefab";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();
	
	
	}

	public override void SpellCast()
	{


		if(!isBeingChanneled)
		{
			timeStarted = Time.time;
			hasStopped = false;
			PlayAnimation ();
			playerAnimationManager.ChangeCastingStatus (true);
			playerController.slowList.Add(slowPercentage);
			spellManager.silenced = true;
			isBeingChanneled = true;
			transform.Find ("FX/LaserShotCharge").gameObject.SetActive (true);
		}



	}


	void BreakChannel()
	{

		lastSpellCastTime = Time.time;
		isBeingChanneled = false;
		playerAnimationManager.ChangeCastingStatus (false);
		playerAnimationManager.ChangeAnimationState ("Idle");
		playerController.slowList.Remove(slowPercentage);
		spellManager.silenced = false;

		transform.Find ("FX/LaserShotCharge").gameObject.SetActive (false);
	}

	void Cast()
	{
		isBeingChanneled = false;
		playerController.slowList.Remove(slowPercentage);
		lastSpellCastTime = Time.time;
		StartGCD ();
		spellManager.silenced = false;
		playerAnimationManager.ChangeCastingStatus (false);
		SpawnPrefab ();
	
		transform.Find ("FX/LaserShotCharge").gameObject.SetActive (false);
	}


	void Update()
	{	

		if(isBeingChanneled)
		{
			if(Time.time > timeStarted + 0.5f && !hasStopped)
			{
				hasStopped = true;
			}
			if(Time.time > timeStarted + channelDuration)
			{
				Cast ();
			}
	

		}
	}

	public override void Cancel()
	{
		
		BreakChannel();
	}

	public override Vector3 GetSpawnLocation()
	{
		return transform.position + transform.forward + new Vector3(0,2,0);
	}

	public override Quaternion GetSpawnRotation()
	{
		return transform.rotation * Quaternion.Euler (90, 0, 0);
	}
}
