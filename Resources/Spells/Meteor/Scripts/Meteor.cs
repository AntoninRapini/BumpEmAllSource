using UnityEngine;
using System.Collections;

public class Meteor : ProjectileSpells {



	public float yOffset = 4.5f;

	public Meteor()
	{
		// Animations
		animationName = "Attaque Distance";
		animationState = 2;

		// Balance
		damage = 15;
		pushPower = 1500;
		range = 25;
		cooldown = 10;
		InGCD = true;
		travelSpeed = 3;
		cost = 500;
		projectileLifeTime = 1;

		// Cast Condtions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());
		castConditions.Add(new NotInCooldown());

		// Infos
		SpellBehavior = "MeteorBehavior";
		spellDescription = "Spawn a meteor on your cursor's location";
		spellName = "Meteor";
		prefabName = "MeteorPrefab";
		prefabPath = "Spells/Meteor/Prefab/MeteorPrefab";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();
	}
		

	public override Vector3 GetSpawnLocation()
	{
		return new Vector3(Cursor.position.x, Cursor.position.y + yOffset , Cursor.position.z);
	}
		
}
