using UnityEngine;
using System.Collections;

public class DeathCoil : ProjectileSpells {


	public DeathCoil()
	{
		// Animations
		animationName = "Attaque Distance";
		animationState = 2;

		// Balance
		InGCD = true;
		travelSpeed = 10;
		damage = 50;
		cooldown = 8;
		cost = 1000;
		projectileLifeTime = 15;

		// Cast Condtions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());
		castConditions.Add(new NotInCooldown());

		// Infos
		spellName = "DeathCoil";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();
		SpellBehavior = "DeathCoilBehavior";
		spellDescription = "Throw a slow projectile that follows the target closest to your cursor, dealing damage to the first target hit";
	
		prefabName = "DeathCoilPrefab";
		prefabPath = "Spells/DeathCoil/Prefab/DeathCoilPrefab";


	}


}
