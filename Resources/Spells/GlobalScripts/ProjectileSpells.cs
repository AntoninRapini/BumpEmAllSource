using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileSpells : Spells
{
    public Vector3 direction;
	public Quaternion rotation;
	public bool foundPrefab;
	public ProjectileBehavior Behavior;
    public Vector3 spawnLocation;
	public List<GameObject> prefabStorage = new List<GameObject>();
    
	public override void SpellCast()
	{
     
		if (InGCD)
		{
			StartGCD ();
		}
		lastSpellCastTime = Time.time;
		PlayAnimation ();
		SpawnPrefab ();
	}

	public virtual void SpawnPrefab()
	{
		
		spawnLocation = GetSpawnLocation();
		rotation = GetSpawnRotation ();
		if(!findPrefab())
		{
			GameObject ProjectileClone = Instantiate(spellPrefab, spawnLocation, rotation) as GameObject;
			ProjectileClone.GetComponent<SpellPrefabBehavior>().LoadVariables(this, gameObject);
			prefabStorage.Add (ProjectileClone);
		}
	}


    public virtual Vector3 GetSpawnLocation()
    {
		return new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
    }

	public virtual Quaternion GetSpawnRotation()
	{
		return Quaternion.identity;
	}




	public virtual bool findPrefab()
	{
		for(int i = 0 ; i < prefabStorage.Count ; i++)
		{
			foundPrefab = true;
			if(prefabStorage[i].gameObject.activeSelf == false)
			{
				SpellSpawn(prefabStorage[i]);
				break;
			}
			else
			{
				foundPrefab = false;
			}
		}
		return foundPrefab;
	}

	public virtual void SpellSpawn(GameObject spellPrefab)
	{
		spellPrefab.transform.position = GetSpawnLocation();
		spellPrefab.transform.rotation = GetSpawnRotation ();
        spellPrefab.gameObject.SetActive (true);
    }


}