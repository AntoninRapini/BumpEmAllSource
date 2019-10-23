using UnityEngine;
using System.Collections;

public class IceShotBehavior : ProjectileBehavior {

	public int freezeDuration = 4;

    void OnEnable()
    {
		if (hasBeenInitialised)
        {
            timeCreated = Time.time;
			GetDirection ();
        }
    }

    void Update()
    {
		gameObject.transform.Translate(direction * projectileSpeed * Time.deltaTime);
        if (Time.time > timeCreated + lifeTime)
        {
            Die();
        }
    }
		
	public override void PlayerHit (Transform player)
	{
		BreakSpellCast (player);
		ApplyFreeze (player);
		Die();
	}
		
	public void ApplyFreeze(Transform playerHit)
	{

		if (playerHit.GetComponent<BreakableSilence>() != null)
		{
			BreakableSilence silence = playerHit.GetComponent<BreakableSilence> ();
			silence.Refresh();
			silence.duration = freezeDuration;
		}
		else
		{
			BreakableSilence silence = playerHit.gameObject.AddComponent<BreakableSilence>();
			silence.duration = freezeDuration;
		}
		if (playerHit.GetComponent<BreakableStun>() != null)
		{
			BreakableStun root = playerHit.GetComponent<BreakableStun> ();
			root.Refresh();
			root.duration = freezeDuration;
		}
		else
		{
			BreakableStun root = playerHit.gameObject.AddComponent<BreakableStun>();
			root.duration = 4;
		}
	}

}
