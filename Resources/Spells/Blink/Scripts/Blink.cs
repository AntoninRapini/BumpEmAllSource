using UnityEngine;
using System.Collections;

public class Blink : Spells {

	public Blink()
	{

		// Animations
		animationName = "Attaque Distance";
		animationState = 2;

		// Balance
		InGCD = false;
		damage = 5;
		range = 5;
		cooldown = 12;
		cost = 1250;

		// Cast Condtions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInCooldown());

		// Infos
		SpellBehavior = "Blink";
		spellDescription = "Teleport instantly in direction of your cursor";
		spellName = "Blink";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();
	

	}

	public override void SpellCast ()
	{
		playerFXs.PlayFX ("Blink");
		lastSpellCastTime = Time.time;
		transform.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		transform.position += (new Vector3(Cursor.position.x, transform.position.y, Cursor.position.z) - transform.position).normalized * range;
	}

}
