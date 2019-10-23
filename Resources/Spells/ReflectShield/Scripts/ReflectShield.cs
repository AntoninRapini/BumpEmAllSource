using UnityEngine;
using System.Collections;

public class ReflectShield : TriggerZoneSpells, Icancelable
{
	// this was at the beginning of spellcast ?
	//transform.position += (new Vector3(Cursor.position.x, transform.position.y, Cursor.position.z) - transform.position).normalized * range;

    public bool shielded;
    public float timeStarted;
    public float duration = 6;
    public float slowPercentage;

    public ReflectShield()
    {
		// Animations
		animationName = "ShieldOn";
		animationState = 5;

		// Balance
		InGCD = true;
		cancellable = true;
		slowPercentage = 0.6f;
		cooldown = 2;
		cost = 700;

		// Cast Condtions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());
		castConditions.Add(new NotInCooldown());

		// Infos
		SpellBehavior = "ReflectShield";
		spellDescription = "Creates a reflective shield for 6 seconds which reflects any projectile shot at it, you walk 20% slower while shield, can be cancelled at any time";
		spellName = "ReflectShield";
		prefabPath = "Spells/ReflectShield/Prefab/Shield";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();


    }


    public override void SpellCast()
    {
        if (!shielded)
        { 
            timeStarted = Time.time;
            shielded = true;
            isBeingChanneled = true;
            Shield();
        }
        else if (shielded && Time.time > timeStarted + 0.2f)
        {
            StopShield();
        }
    }


    void Shield()
    {
		playerController.slowList.Add(slowPercentage);
		playerAnimationManager.ChangeCastingStatus (true);
		playerAnimationManager.ChangeAnimationState (animationName);
      	spellManager.silenced = true;
		SpawnPrefab ();
		PlaceShield ();
    }
		
    void StopShield()
    {
		StartGCD ();
        shielded = false;
        lastSpellCastTime = Time.time;
        isBeingChanneled = false;
		playerAnimationManager.ChangeCastingStatus (false);
      	playerController.slowList.Remove(slowPercentage);
		if(prefab != null)
		{
			prefab.SetActive(false);
		}
       spellManager.silenced = false;
    }


    void Update()
    {
        if(shielded)
        {
            if (Time.time > timeStarted + duration)
            {
                StopShield();
            }
        }
    }

	void PlaceShield()
	{
		prefab.transform.SetParent(transform);
		prefab.transform.localPosition = GetSpawnLocation ();
		prefab.transform.localRotation = GetSpawnRotation ();
	}


    public override void Cancel()
    {
        StopShield();
    }

	public override Vector3 GetSpawnLocation()
	{
		return new Vector3(0, 0.7f, 0.45f);
	}
}
