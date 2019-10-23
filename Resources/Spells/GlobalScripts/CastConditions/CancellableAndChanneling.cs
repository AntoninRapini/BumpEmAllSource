using UnityEngine;
using System.Collections;

public class CancellableAndChanneling : CastCondition {

	public override bool ConditionMet(Spells spell)
	{
		if(spell.cancellable && spell.isBeingChanneled)
		{
			Debug.Log ("cancellable and being channelled");
			return true;
		}


		for(int i = 0; i < spell.castConditions.Count; i++)
		{
			if(spell.castConditions[i] != this && !spell.castConditions[i].ConditionMet(spell))
			{
				return false;
			}
		}

		return true;
	}
}
