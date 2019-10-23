using UnityEngine;
using System.Collections;

public static class MardelTransformExtensions
{
   
    public static void SetX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public static void SetY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public static void SetZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

	public static void ResetPosition(this Transform transform)
	{
		transform.position = Vector3.zero;
	}

	public static void SetPosition(this Transform transform, Vector3 position)
	{
		transform.position = position;
	}

	public static Vector3 SetNormalizedDirection(this Transform transform, Transform target)
	{
		Vector3 direction = target.position - transform.position;
		direction = direction.normalized;
		return direction;
	}

	public static Vector3 SetNormalizedDirection(this Transform transform, Vector3 target)
	{
		Vector3 direction = target - transform.position;
		direction = direction.normalized;
		return direction;
	}

	public static void ResetRotation(this Transform transform)
	{
		transform.rotation = Quaternion.identity;
	}

	public static void SetRotation(this Transform transform, Quaternion rotation)
	{
		transform.rotation = rotation;
	}

	public static void SetRotation(this Transform transform, Vector3 rotation)
	{
		transform.rotation = Quaternion.Euler(rotation);
	}

}
