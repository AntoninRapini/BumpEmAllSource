using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour {

	private PlayerController playerController;
	public Animator animator;


	void Start()
	{
		playerController = transform.GetComponent<PlayerController> ();
	}


	public void ChangeAnimationState(string animationState)
	{
		animator.CrossFade (animationState, 0);
	}

	public void ChangeCastingStatus(bool status)
	{
		animator.SetBool ("casting", status);
	}
}
