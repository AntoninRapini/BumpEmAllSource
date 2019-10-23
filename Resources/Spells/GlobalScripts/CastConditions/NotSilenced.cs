using UnityEngine;
using System.Collections;

public class NotSilenced : CastCondition {

	public override bool ConditionMet(Spells spell)
	{
		if(spell.spellManager.silenced)
		{  
			if(spell.cancellable && spell.isBeingChanneled)
			{
				return true;
			}
			return false;
		}

		return true;

	}

}
