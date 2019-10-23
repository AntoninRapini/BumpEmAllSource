using UnityEngine;
using System.Collections;

public class RockStance : Spells {


    public bool transformed;
    public float timeStarted;
    public float duration = 2;
    public float desiredSlowPercentage = 0;

    public RockStance()
    {

		// Animations
		animationName = "Attaque Distance";
		animationState = 2;

		// Balance
		InGCD = false;
		cost = 1000;
		cooldown = 20;

		// Cast Condtions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInCooldown());

		// Infos
		SpellBehavior = "RockStance";
		spellDescription = "Transform into a stone, stopping any previous movement you had and making you invulnerable for 2 seconds, pushes ennemies around you when you transform back";
		spellName = "RockStance";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();
   
    }


    public override void SpellCast()
    {
        if (!transformed)
        {
            EnterRockStance();
        }
    }


    void EnterRockStance()
    {
       
        transformed = true;
        timeStarted = Time.time;
        lastSpellCastTime = Time.time;
		playerFXs.PlayFX ("RockStance");
   

        transform.GetComponent<Collider>().enabled = false;
		playerAnimationManager.animator.gameObject.SetActive(false);
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
      	spellManager.silenced = true;
     	playerController.slowList.Add(desiredSlowPercentage);
       
    }

    void QuitRockStance()
    {
		playerFXs.StopFX ("RockStance");
		playerAnimationManager.animator.gameObject.SetActive(true);
		playerController.slowList.Remove(desiredSlowPercentage);
       	spellManager.silenced = false;
        transform.GetComponent<Collider>().enabled = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;
    }



    void Update()
    {
        if (transformed)
        {
            if (Time.time > timeStarted + duration)
            {
				playerFXs.PlayFX ("RockStanceQuit");
                transformed = false;
                QuitRockStance();
            }
        }

    }

	public override void Cancel()
	{
		QuitRockStance ();
	}
    
}
