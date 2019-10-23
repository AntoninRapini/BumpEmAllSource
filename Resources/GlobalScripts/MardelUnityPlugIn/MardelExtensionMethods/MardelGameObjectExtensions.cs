using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MardelGameObjectExtensions  {


	public static void SetInactive(this GameObject gameObject)
	{
		gameObject.SetActive (false);
	}

	public static void SetActive(this GameObject gameObject)
	{
		gameObject.SetActive (true);
	}

	public static void ChangeActiveState(this GameObject gameObject)
	{
		if(gameObject.activeSelf)
		{
			gameObject.SetInactive ();
		}
		else
		{
			gameObject.SetActive ();
		}
	}


	public static GameObject GetClosestGameObjectInList(this GameObject gameObject, List<GameObject> list)
	{
		float distance = 0;
		GameObject closestObject = gameObject;
		for(int i = 0; i < list.Count; i++)
		{
			if(Vector3.Distance(gameObject.transform.position, list[i].transform.position) < distance || distance == 0)
			{
				distance = Vector3.Distance (gameObject.transform.position,  list[i].transform.position);
				closestObject =  list[i];
			}
		}
		return closestObject;
	}

	public static GameObject GetClosestGameObjectInList(this GameObject gameObject,List<GameObject> list, GameObject excludedFromSearch)
	{
		float distance = 0;
		GameObject closestObject = gameObject;
		for(int i = 0; i < list.Count; i++)
		{
			if(list[i] != excludedFromSearch)
			{
				if(Vector3.Distance(gameObject.transform.position, list[i].transform.position) < distance || distance == 0)
				{
					distance = Vector3.Distance (gameObject.transform.position, list[i].transform.position);
					closestObject = list [i];
				}
			}
		}
		return closestObject;
	}

	public static GameObject GetClosestGameObjectInList(this GameObject gameObject, GameObject[] list)
	{
		float distance = 0;
		GameObject closestObject = gameObject;
		for(int i = 0; i < list.Length; i++)
		{
			if(Vector3.Distance(gameObject.transform.position, list[i].transform.position) < distance || distance == 0)
			{
				distance = Vector3.Distance (gameObject.transform.position,  list[i].transform.position);
				closestObject =  list[i];
			}
		}
		return closestObject;
	}

	public static GameObject GetClosestGameObjectInList(this GameObject gameObject,GameObject[] list, GameObject excludedFromSearch)
	{
		float distance = 0;
		GameObject closestObject = gameObject;
		for(int i = 0; i < list.Length; i++)
		{
			if(list[i] != excludedFromSearch)
			{
				if(Vector3.Distance(gameObject.transform.position, list[i].transform.position) < distance || distance == 0)
				{
					distance = Vector3.Distance (gameObject.transform.position, list[i].transform.position);
					closestObject = list [i];
				}
			}
		}
		return closestObject;
	}
}
