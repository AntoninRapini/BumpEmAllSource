using UnityEngine;
using System.Collections;

public class NotInCooldown : CastCondition {

	public override bool ConditionMet(Spells spell)
	{
		if(Time.time > spell.lastSpellCastTime + spell.cooldown || spell.lastSpellCastTime == 0)
		{
			return true;
		}

		return false;
	}
}
