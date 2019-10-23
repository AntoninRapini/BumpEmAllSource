using UnityEngine;
using System.Collections;

public class Mine : TriggerZoneSpells {



	public Mine()
	{
		// Animation
		animationName = "Attaque Distance";
		animationState = 2; 

		// Balance
		InGCD = true;
		damage = 15;
		pushPower = 1200;
		range = 25;
		cooldown = 0;
		cost = 1000;

		// Cast Conditions
		castConditions.Add(new NotSilenced());
		castConditions.Add(new NotInGlobalCooldown());
		castConditions.Add(new NotFalling());

		// Infos
		SpellBehavior = "MineBehavior";
		spellDescription = "Places a mine under the player that activates after one second and explodes if anyone steps on it, you can have only one mine active at a time";
		spellName = "Mine";
		prefabName = "MinePrefab";
		prefabPath = "Spells/Mine/Prefab/MinePrefab";
		iconPath = "UI/Game/PlayerUI/Sprites/Spells/" + spellName.ToString ();

	}



	public override Vector3 GetSpawnLocation()
	{
		return  new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
	}

	public override void SpellSpawn(GameObject spellPrefab)
	{
		spellPrefab.transform.position = GetSpawnLocation();
		spellPrefab.transform.rotation = GetSpawnRotation ();
		spellPrefab.gameObject.SetActive (true);
		spellPrefab.GetComponent<MineBehavior> ().Reset ();
	}
		
	public override void Cancel()
	{
		
			if(prefab != null && prefab.activeSelf == true)
			{
				prefab.GetComponent<MineBehavior> ().Die ();
			}
	}

}
