using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SimpleRaycastHit
{
	public Transform transform;
	public SimpleRayCastable collider;
	public float distance;
	public SimpleRaycastHit (Ray ray, SimpleRayCastable simpleRayCastable)
	{
		transform = simpleRayCastable.transform;
		collider = simpleRayCastable;
		distance = Vector3.Distance(transform.position, ray.origin);
	}
}
