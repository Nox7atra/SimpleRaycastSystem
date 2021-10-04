using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SimpleRaycastHit
{
	public Transform transform;
	public SimpleRayCastable collider;
	public SimpleRaycastHit (SimpleRayCastable simpleRayCastable)
	{
		transform = simpleRayCastable.transform;
		collider = simpleRayCastable;
	}
}
