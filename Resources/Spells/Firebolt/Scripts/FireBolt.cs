using UnityEngine;
using System.Collections;

public class FireBolt : ProjectileSpells 
{

	public FireBolt()
	{
		// Animations
		animationName = "Attaque Distance";
		animationState = 2;

		// Balance
        InGCD = true;
		damage = 6;
		pushPower = 800;
		cooldown = 0;
		travelSpeed = 25;
		projectileLifeTime = 5;
		cost = 350;

		// Cast Conditions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());


		// Infos
        SpellBehavior = "FireboltBehavior";
		spellDescription = "Throw a rock in the direction of your cursor, damaging and pushing the first player hit";
		spellName = "FireBolt";
		prefabName = "FireboltPrefab";
		prefabPath = "Spells/Firebolt/Prefab/FireboltPrefab";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();

    }
		



}
