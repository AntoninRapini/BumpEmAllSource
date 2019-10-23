using UnityEngine;
using System.Collections;

public class NotFalling : CastCondition {


	public override bool ConditionMet(Spells spell)
	{
		if(spell.transform.GetComponent<Rigidbody>().velocity.y <= 0)
		{
			return true;
		}

		return false;
	}
}
