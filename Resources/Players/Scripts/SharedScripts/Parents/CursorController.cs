using UnityEngine;
using System.Collections;

public class CursorController : MonoBehaviour {

	public float cursorSpeed;
	[HideInInspector] public Transform player;
	protected GameInputController input;
	protected Vector3 oldPosition, newPosition;
	protected RaycastHit rayHit;
	protected float currentY;
	public LayerMask myLayerMask;
	[HideInInspector] public Vector3 floorPointHit;
	protected Vector3 nextPosition;
	protected Vector3 oldCursorPosition;

}
