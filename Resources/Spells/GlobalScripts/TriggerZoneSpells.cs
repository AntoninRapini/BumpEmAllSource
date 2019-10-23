using UnityEngine;
using System.Collections;

public class TriggerZoneSpells : ProjectileSpells {


	public GameObject prefab;

	public override void SpawnPrefab()
	{
		spawnLocation = GetSpawnLocation();
		rotation = GetSpawnRotation ();
		if(!findPrefab())
		{
			prefab = Instantiate(spellPrefab, spawnLocation, rotation) as GameObject;
			prefab.GetComponent<SpellPrefabBehavior>().LoadVariables(this, gameObject);
		}
	}


	public override bool findPrefab()
	{
		if(prefab != null)
		{
			SpellSpawn (prefab);
			return true;
		}
		return false;
	}



}
