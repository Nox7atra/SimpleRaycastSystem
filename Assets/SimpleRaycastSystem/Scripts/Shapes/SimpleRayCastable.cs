using UnityEngine;

public abstract class SimpleRayCastable : MonoBehaviour
{
	protected virtual void OnEnable ()
	{
		SimpleRaycastSystem.Register(this);
	}
	protected virtual void OnDisable ()
	{
		SimpleRaycastSystem.OnRelease(this);
	}

	public abstract bool CheckIntersection (Ray ray);

}
