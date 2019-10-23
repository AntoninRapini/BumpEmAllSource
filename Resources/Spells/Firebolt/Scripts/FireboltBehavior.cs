using UnityEngine;
using System.Collections;

public class FireboltBehavior : ProjectileBehavior {


    void OnEnable()
    {
		if(hasBeenInitialised)
		{
			timeCreated = Time.time;
			GetDirection ();
		}  
    }

	void Update()
	{
		transform.Translate(direction * projectileSpeed * Time.deltaTime);
        if(Time.time > timeCreated + lifeTime)
        {
            Die();
        }
	}
		
	public override void PlayerHit (Transform player)
	{
		player.GetComponent<PlayerController>().Push(transform.position, spell.pushPower, spellCreator);
		player.GetComponent<Player>().TakeDamage(spell.damage, spellCreator);
		player.GetComponent<PlayerFX> ().PlayFX ("Hit");
		Die();
	}


	

}
