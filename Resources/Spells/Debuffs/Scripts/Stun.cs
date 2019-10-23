using UnityEngine;
using System.Collections;

public class Stun : Debuff {

	private float slowPercentage = 0;
	public bool hasStopped;
	private PlayerController playerController;
	void Start()
	{
		playerController = transform.GetComponent<PlayerController>();
		playerController.animator.SetBool ("isCasting", true);
		playerController.animator.SetInteger ("Character State", 7);
		transform.GetComponent<PlayerFX> ().PlayFX ("Stun");
		transform.GetComponent<Player>().debuffDictionary.Add("Stun", this);
		timeAppeared = Time.time;
		playerController.slowList.Add(slowPercentage);
		playerController.lookAtCursor = false;
		transform.GetComponent<SpellManager>().silenced = true;
	}

	public Stun(int _duration) : base(_duration)
	{
		duration = _duration;
	}
	void LateUpdate()
	{
		if(Time.time > timeAppeared + 0.5f && !hasStopped)
		{
			hasStopped = true;
			playerController.animator.speed = 0;
		}
		if (Time.time > timeAppeared + duration)
		{
			Break();
		}
	}


	public override void Break()
	{
		playerController.animator.SetBool ("isCasting", false);
		playerController.animator.SetInteger ("Character State", 0);
		playerController.animator.CrossFade ("Idle", 0);
		transform.GetComponent<PlayerFX> ().StopFX ("Stun");
		playerController.animator.speed = 1;
		transform.GetComponent<SpellManager>().silenced = false;
		playerController.lookAtCursor = true;
		playerController.slowList.Remove(slowPercentage);
		transform.GetComponent<Player>().debuffDictionary.Remove("Stun");
		playerController.moveSpeed = playerController.baseMoveSpeed;
		Destroy(this);
	}

	public override void Refresh()
	{
		timeAppeared = Time.time;
	}

}
