using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// This Script creates different arrays each containing all skins for one spot, and creates a dictionary containing all skins from these arrays and a boolean that tells us
// if a skin is available to pick (true if available).
public class LoadSkins : MonoBehaviour {


	public static LoadSkins loadSkins;
	//[HideInInspector]
	public GameObject[] torsoModels, weaponModels, headModels;
	public Dictionary<GameObject, bool> skins = new Dictionary<GameObject, bool>();


	// Singleton Pattern 
	void Awake()
	{
		DontDestroyOnLoad (gameObject);
		if(loadSkins == null)
		{
			loadSkins = this;
		}
		else
		{
			Destroy (gameObject);
		}
		LoadArrayAndDictionary ("Skins/Prefabs/Torso", ref torsoModels);
		LoadArrayAndDictionary ("Skins/Prefabs/Weapon", ref weaponModels);
		LoadArrayAndDictionary ("Skins/Prefabs/Head",ref  headModels);
	}


	// function responsible for filling the arrays and the dictionary
	public void LoadArrayAndDictionary(string path, ref GameObject[] array)
	{
		array = Resources.LoadAll (path, typeof(GameObject)).Cast<GameObject>().ToArray();

		if(array[0].name != "Default")
		{
			for(int i = 0; i < array.Length; i++)
			{
				if(array[i].name == "Default")
				{
					GameObject temporaryReference = array [0];
					array [0] = array [i];
					array [i] = temporaryReference;
				}
			}
		}
	
		for(int i = 0; i < array.Length; i++)
		{
			skins.Add(array[i], true);
		}
	}

	public void ResetSkinDictionary()
	{
		foreach(GameObject key in skins.Keys.ToList())
		{
			skins [key] = true;
		}
	}

}
