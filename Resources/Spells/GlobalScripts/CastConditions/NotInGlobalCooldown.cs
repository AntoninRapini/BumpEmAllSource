using UnityEngine;
using System.Collections;

public class NotInGlobalCooldown : CastCondition {

	public override bool ConditionMet(Spells spell)
	{
		if(!spell.spellManager.PlayerInGlobalCooldown())
		{
			return true;
		}
		return false;
	}
}
