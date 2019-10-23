using UnityEngine;
using System.Collections;

public class DragonBreath : TriggerZoneSpells {


    public DragonBreath()
    {
		// Animations
		animationName = "Attaque Distance";
		animationState = 2;

		// Balance
		InGCD = true;
		damage = 10;
		pushPower = 0;
		cooldown = 20;
		cost = 1250;
		projectileLifeTime = 1;

		// Cast Condtions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());
		castConditions.Add(new NotInCooldown());

		// Infos
		SpellBehavior = "DragonBreathBehavior";
		spellDescription = "Casts a flame in a cone in front of the player, damaging and stunning any enemy hit";
		spellName = "DragonBreath";
		prefabName = "DragonBreathPrefab";
		prefabPath = "Spells/DragonBreath/Prefab/DragonBreathPrefab";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();

    }


	public override Vector3 GetSpawnLocation()
	{
		return new Vector3 ( transform.position.x + 0, transform.position.y + 0.45f,  transform.position.z + 0.2f);
	}

	public override Quaternion GetSpawnRotation ()
	{
		return transform.rotation;
	}



}
