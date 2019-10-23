using UnityEngine;
using System.Collections;

public class IceShot : ProjectileSpells
{
    public IceShot()
    {
		// Animations
		animationName = "Attaque Distance";
		animationState = 2;

		// Balance
        InGCD = true;
        cooldown = 12;
        travelSpeed = 30;
		projectileLifeTime = 5;
		cost = 700;
		cooldown = 12;

		// Cast Conditions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());
		castConditions.Add(new NotInCooldown());

		// Infos
        SpellBehavior = "Ice";
        spellDescription = "Throw an Ice projectile towards your cursor, freezing the first enemy hit";
        spellName = "IceShot";
        prefabName = "IceShotPrefab";
		prefabPath = "Spells/IceShot/Prefab/IceShotPrefab";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();

    }
}
