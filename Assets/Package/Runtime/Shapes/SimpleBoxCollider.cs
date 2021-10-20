using System;
using UnityEngine;

public class SimpleBoxCollider : SimpleRayCastable
{
    public Vector3 center;
    public Vector3 size = new Vector3(1,1,1);
    private Transform _transform;
    private void Awake ()
    {
        _transform = transform;
    }

    public override bool CheckIntersection (Ray ray, out Vector3 hitPosition)
    {
        hitPosition = new Vector3();
        var matrix = transform.localToWorldMatrix;
        var rpos = ray.origin;
        var rdir =  ray.direction;
        var pos = matrix.MultiplyPoint3x4(center);
        var halfScale = matrix.MultiplyVector(size / 2);
        Vector3 vmin =  pos - halfScale;
        Vector3 vmax =  pos + halfScale;
        float t1 = (vmin.x - rpos.x) / rdir.x;
        float t2 = (vmax.x - rpos.x) / rdir.x;
        float t3 = (vmin.y - rpos.y) / rdir.y;
        float t4 = (vmax.y - rpos.y) / rdir.y;
        float t5 = (vmin.z - rpos.z) / rdir.z;
        float t6 = (vmax.z - rpos.z) / rdir.z;
        float t7 = Mathf.Max(Mathf.Max(Mathf.Min(t1, t2), Mathf.Min(t3,t4)), Mathf.Min(t5, t6));
        float t8 = Mathf.Min(Mathf.Min(Mathf.Max(t1, t2), Mathf.Max(t3, t4)), Mathf.Max(t5, t6));
        return ! (t8 < 0 || t7 > t8);
    }
    private Vector2 CalcMinMax (float direction, float min, float max, float origin)
    {
        if (direction >= 0)
        {
            return new Vector2(
                (min - origin) / direction,
                (max - origin) / direction
            );
        }
        return new Vector2(
            (max - origin) / direction,
            (min - origin) / direction
        );
    }
    
    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        var matrix = transform.localToWorldMatrix;
        var pos = matrix.MultiplyPoint3x4(center);
        var scale = matrix.MultiplyVector(size / 2)* 2;
    
        Gizmos.DrawWireCube(pos, scale);
    }
}
