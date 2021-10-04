using UnityEngine;

public class SimpleSphereCollider : SimpleRayCastable
{
    public Vector3 center;
    public float raduis = 1;
    public override bool CheckIntersection (Ray ray)
    {
        var matrix = transform.localToWorldMatrix;
        var pos = matrix.MultiplyPoint3x4(center);
        var originCenterVector = ray.origin - pos;
        var direction = ray.direction;
        
        float a = Vector3.Dot(direction, direction);
        float b = 2f * Vector3.Dot(originCenterVector, direction);
        float c = Vector3.Dot(originCenterVector, originCenterVector) - raduis * raduis;
        
        float discriminant = b * b - 4 * a * c;

        if (discriminant < 0) return false;
        
        float numerator = -b - Mathf.Sqrt(discriminant);
        
        if (numerator > 0) return true;
        
        numerator = -b + Mathf.Sqrt(discriminant);
        
        if (numerator > 0) return true;

        return false;
    }
    
    private void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        var matrix = transform.localToWorldMatrix;
        var pos = matrix.MultiplyPoint3x4(center);
        Gizmos.DrawWireSphere(pos, raduis);
    }
}
