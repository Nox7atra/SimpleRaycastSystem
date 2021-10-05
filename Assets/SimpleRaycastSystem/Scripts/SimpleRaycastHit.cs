using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SimpleRaycastHit
{
	public Transform transform;
	public SimpleRayCastable collider;
	public float distance;
	public Vector3 point;
	public SimpleRaycastHit (Ray ray, SimpleRayCastable simpleRayCastable, Vector3 hitPosition)
	{
		transform = simpleRayCastable.transform;
		collider = simpleRayCastable;
		point = hitPosition;
		distance = Vector3.Distance(transform.position, ray.origin);
	}
}
