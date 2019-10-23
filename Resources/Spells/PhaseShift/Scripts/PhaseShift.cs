using UnityEngine;
using System.Collections;

public class PhaseShift : Spells {

	public float slowPercentage = 0;
	public float duration = 2, timeStarted;
	public PhaseShift()
	{
		// Animaton
		animationName = "Attaque Distance";
		animationState = 2;

		// Balance
		InGCD = false;
		cooldown = 6;
		cancellable = true;
		cost = 500;

		// Cast Conditions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInCooldown());

		// Infos
		SpellBehavior = "PhaseShift";
		spellDescription = "Dissapear for 2 seconds, avoiding any spells, can't use while pushed";
		spellName = "PhaseShift";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();
	

	}

	public override void SpellCast ()
	{

		if(transform.GetComponent<Rigidbody>().velocity.magnitude < 0.5f && !isBeingChanneled)
		{
			EnterPhaseShift ();
		}
		else if(isBeingChanneled && Time.time > timeStarted + 0.2f)
		{
			QuitPhaseShift();
		}


	}

	void EnterPhaseShift()
	{
		playerFXs.PlayFX ("PhaseShift");

		timeStarted = Time.time;
		isBeingChanneled = true;
		StartCoroutine ("CountDownQuit");

		playerController.slowList.Add(slowPercentage);

		transform.GetComponent<Collider> ().enabled = false;
		playerAnimationManager.animator.gameObject.SetActive(false);
		transform.GetComponent<Rigidbody> ().isKinematic = true;


		spellManager.silenced = true;

	}
		
	void QuitPhaseShift()
	{
		playerController.slowList.Remove (slowPercentage);
		isBeingChanneled = false;
		transform.GetComponent<Collider> ().enabled = true;
		playerAnimationManager.animator.gameObject.SetActive(true);
		transform.GetComponent<Rigidbody> ().isKinematic = false;
		spellManager.silenced = false;
		lastSpellCastTime = Time.time;
	}



	IEnumerator CountDownQuit()
	{
		yield return new WaitForSeconds (duration);
		if(isBeingChanneled)
		{
			QuitPhaseShift ();
		}

	}

	public override void Cancel()
	{
		StopCoroutine ("CountDownQuit");
		QuitPhaseShift ();
	}
}
