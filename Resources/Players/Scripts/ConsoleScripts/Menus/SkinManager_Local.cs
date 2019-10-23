using UnityEngine;
using System.Collections;

public class SkinManager_Local : MonoBehaviour {



    // Skins
    [HideInInspector] public Transform skinTypeSelected;
    private GameObject currentTorsoSkin, currentHeadSkin, currentWeaponSkin;
    public Transform torsoSpot, headSpot, weaponSpot;
    private int placeInTorsoArray, placeInHeadArray, placeInWeaponArray;
   
    
    public void SaveSkins(ref PlayerInstance_Local player)
    {
        player.placeInTorsoArray = placeInTorsoArray;
        player.placeInHeadArray = placeInHeadArray;
        player.placeInWeaponArray = placeInWeaponArray;
        player.torsoSkin = currentTorsoSkin;
        player.weaponSkin = currentWeaponSkin;
        player.headSkin = currentHeadSkin;
    }

    public void RemoveCurrentSkins()
    {
        LoadSkins.loadSkins.skins[currentTorsoSkin] = true;
        LoadSkins.loadSkins.skins[currentWeaponSkin] = true;
        LoadSkins.loadSkins.skins[currentHeadSkin] = true;
    }

    public void ChangeSkin(int direction)
    {
        switch (skinTypeSelected.name)
        {
            case "Torso":
                SelectSkin(ref placeInTorsoArray, direction, LoadSkins.loadSkins.torsoModels, ref currentTorsoSkin, ref torsoSpot);
                break;
            case "Head":
                SelectSkin(ref placeInHeadArray, direction, LoadSkins.loadSkins.headModels, ref currentHeadSkin, ref headSpot);
                break;
            case "Weapon":
                SelectSkin(ref placeInWeaponArray, direction, LoadSkins.loadSkins.weaponModels, ref currentWeaponSkin, ref weaponSpot);
                break;
            default:
                break;
        }
    }

    // Selects a skin from the specified array and checks if it is available unless it's the default and equips it, if not it checks if the one after is available
    public void SelectSkin(ref int placeInArray, int directionInArray, GameObject[] currentSkinArray, ref GameObject currentSkin, ref Transform model)
    {
        if (currentSkinArray.Length > 1)
        {
            bool available;
            if (placeInArray == 0 && directionInArray == -1)
            {
                placeInArray = currentSkinArray.Length - 1;
            }
            else if (placeInArray == currentSkinArray.Length - 1 && directionInArray == 1)
            {
                placeInArray = 0;
            }
            else
            {
                placeInArray += directionInArray;
            }

            LoadSkins.loadSkins.skins.TryGetValue(currentSkinArray[placeInArray], out available);
            if (available)
            {
                if (placeInArray == 0)
                {
                    LoadSkins.loadSkins.skins[currentSkin] = true;
                    model.GetComponent<MeshFilter>().mesh = null;
                    currentSkin = currentSkinArray[placeInArray];
                }
                else
                {
                    LoadSkins.loadSkins.skins[currentSkinArray[placeInArray]] = false;
                    LoadSkins.loadSkins.skins[currentSkin] = true;
                    currentSkin = currentSkinArray[placeInArray];
                    EquipSkin(model, currentSkin);

                }
            }
            else
            {
                SelectSkin(ref placeInArray, directionInArray, currentSkinArray, ref currentSkin, ref model);
            }
        }
        else
        {
            currentSkin = currentSkinArray[0];
        }
    }

    public void SetSkinsToDefault()
    {
        placeInTorsoArray = 0;
        placeInHeadArray = 0;
        placeInWeaponArray = 0;

        currentTorsoSkin = LoadSkins.loadSkins.torsoModels[0];
        currentHeadSkin = LoadSkins.loadSkins.headModels[0]; ;
        currentWeaponSkin = LoadSkins.loadSkins.weaponModels[0]; ;

        torsoSpot.GetComponent<MeshFilter>().mesh = null;
        weaponSpot.GetComponent<MeshFilter>().mesh = null;
        headSpot.GetComponent<MeshFilter>().mesh = null; 
    }

    public void EquipSkin(Transform model, GameObject currentSkin)
    {
		if(currentSkin.transform.GetComponent<MeshFilter>() != null)
		{
			model.GetComponent<MeshFilter>().mesh = currentSkin.transform.GetComponent<MeshFilter>().sharedMesh;
		}
		if(currentSkin.transform.GetComponent<MeshRenderer>() != null)
		{
			model.GetComponent<MeshRenderer>().material = currentSkin.transform.GetComponent<MeshRenderer>().sharedMaterial;
		}
    }

    public void RetrieveSkins(PlayerInstance_Local player)
    {
        placeInWeaponArray = player.placeInWeaponArray;
        currentWeaponSkin = player.weaponSkin;
        EquipSkin(weaponSpot, currentWeaponSkin);

        placeInHeadArray = player.placeInHeadArray;
        currentHeadSkin = player.headSkin;
        EquipSkin(headSpot, currentHeadSkin);

        placeInTorsoArray = player.placeInTorsoArray;
        currentTorsoSkin = player.torsoSkin;
        EquipSkin(torsoSpot, currentTorsoSkin);

    }
}
