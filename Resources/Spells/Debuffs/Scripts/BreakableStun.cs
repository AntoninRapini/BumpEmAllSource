using UnityEngine;
using System.Collections;

public class BreakableStun : Debuff , IBreakable
{

    private float slowPercentage = 0;
	private PlayerController playerController;
    void Start()
    {
		playerController = transform.GetComponent<PlayerController> ();
        transform.GetComponent<Player>().debuffDictionary.Add("BreakableRoot", this);
		playerController.animator.speed = 0;
		playerController.animator.gameObject.SetActive (false);
        timeAppeared = Time.time;
		transform.GetComponent<PlayerFX> ().PlayFX ("IceBlock");
		playerController.slowList.Add(slowPercentage);
		playerController.lookAtCursor = false;
    }

    public BreakableStun(int _duration) : base(_duration)
    {
        duration = _duration;
    }
    void LateUpdate()
    {

        if (Time.time > timeAppeared + duration)
        {
            Break();
        }
    }


    public override void Break()
    {
		
		transform.GetComponent<PlayerFX> ().StopFX ("IceBlock");
		playerController.animator.gameObject.SetActive (true);
		playerController.animator.speed = 1;
		playerController.lookAtCursor = true;
		playerController.slowList.Remove(slowPercentage);
        transform.GetComponent<Player>().debuffDictionary.Remove("BreakableRoot");
		playerController.moveSpeed = transform.GetComponent<PlayerController>().baseMoveSpeed;
        Destroy(this);
    }

    public override void Refresh()
    {
        timeAppeared = Time.time;
    }

}