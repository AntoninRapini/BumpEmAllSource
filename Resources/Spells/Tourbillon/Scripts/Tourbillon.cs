using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tourbillon : Spells, Icancelable {


	public bool Spinning;
	public int damageDealt;
	public float timeStarted;

	public float moveSpeedBuff = 1.2f;
	public SphereCollider spinningCollision;
	public List<GameObject> hitList = new List<GameObject> ();

	public float timeBetweenTicks = 1, lastTickTime;
	public Tourbillon()
	{
		// Animations
		animationName = "Tourbillon";
		animationState = 6;

		// Balance
		InGCD = true;
		cooldown = 0;
		pushPower = 300;
		cancellable = true;
		cost = 700;

		// Cast Condtions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());
		castConditions.Add(new NotInCooldown());

		// Infos
		SpellBehavior = "Tourbillon";
		spellDescription = "Spin to win, dealing damage to all ennemies hit and to yourself while spinning, pushes ennemies when stopped relative to the damage dealt";
		spellName = "Tourbillon";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();

	}

	public override void SpellCast ()
	{
		if(!Spinning)
		{
			
			isBeingChanneled = true;
			Spinning = true;
			StartSpinning ();
		}
		else if(Spinning &&  Time.time > timeStarted + 0.2f)
		{
			playerFXs.PlayFX ("TourbillonEnd");
		
			PushPlayers();
			QuitSpinning ();
		}
	}


	void PushPlayers()
	{
		for(int i = 0; i < hitList.Count; i++)
		{
			hitList [i].transform.GetComponent<PlayerController>().Push(transform.position, pushPower, gameObject);
		}
      
	}
	void QuitSpinning()
	{
		playerFXs.StopFX ("Tourbillon");
		playerFXs.StopFX ("Tourbillon2");
		StartGCD ();
		isBeingChanneled = false;
        Spinning = false;
		hitList.Clear ();
		Destroy (spinningCollision);
		spellManager.silenced = false;
		playerController.slowList.Remove (moveSpeedBuff);
		playerAnimationManager.ChangeCastingStatus (false);
		playerController.lookAtCursor = true;
		playerAnimationManager.animator.transform.rotation = transform.rotation;

	}

	void StartSpinning()
	{
		pushPower = 300;
		damageDealt = 3;
		lastTickTime = 0;
		playerController.slowList.Add (moveSpeedBuff);
		playerFXs.PlayFX ("Tourbillon");
		playerFXs.PlayFX ("Tourbillon2");
		playerController.lookAtCursor = false;
		playerAnimationManager.ChangeAnimationState (animationName);
		playerAnimationManager.ChangeCastingStatus (true);
		spellManager.silenced = true;
		timeStarted = Time.time;
		spinningCollision = gameObject.AddComponent<SphereCollider> ();
		spinningCollision.isTrigger = true;
		spinningCollision.radius = 2;
		spinningCollision.center = new Vector3 (0, 0.89f, 0);

	}

	void CheckHit()
	{
		lastTickTime = Time.time;
		player.TakeDamage (2, gameObject);

		for(int i = 0; i < hitList.Count; i++)
		{
			damageDealt += 1;
			pushPower += 300;
			damageDealt = Mathf.Clamp (damageDealt, 3, 6);
			pushPower = Mathf.Clamp (pushPower, 300, 1500);
		}

		for(int i = 0; i < hitList.Count; i++)
		{
			hitList [i].GetComponent<Player> ().TakeDamage (damageDealt, gameObject);
			hitList [i].GetComponent<PlayerFX> ().PlayFX ("TourbillonHit");
		}
	}

	void OnTriggerStay(Collider col)
	{
		if (Spinning)
		{
			if (col.tag == "Player" && col.transform != transform) 
			{
				if (Time.time > lastTickTime + timeBetweenTicks || lastTickTime == 0)
				{
					CheckHit ();
				}
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (Spinning) 
		{
			if (col.CompareTag("Player") && col.transform != transform) 
			{
				if (!hitList.Contains (col.gameObject))
				{
					hitList.Add (col.gameObject);
				}
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(Spinning)
		{
			if(col.CompareTag("Player") && col.transform != transform)
			{
				if(hitList.Contains(col.gameObject))
				{
					hitList.Remove (col.gameObject);
				}
			}
		}
	
	}



    public override void Cancel()
    {
        QuitSpinning();
    }

}
