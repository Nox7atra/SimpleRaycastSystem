using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SimpleRaycastSystem
{
    private static List<SimpleRayCastable> _SimpleRayCastables = new List<SimpleRayCastable>();
    private static List<SimpleRaycastHit> _CurrentHitsBuffer = new List<SimpleRaycastHit>();
    
    public static bool Raycast (Ray ray, out SimpleRaycastHit hit, LayerMask? mask = null)
    {
        IEnumerable<SimpleRaycastHit> hits;
        if (RaycastAll(ray, out hits, mask))
        {
            hit = hits.First();
            return true;
        } else
        {
            hit = new SimpleRaycastHit();
            return false;
        }
    }
    public static bool RaycastAll(Ray ray, out IEnumerable<SimpleRaycastHit> hits, LayerMask? mask = null)
    {
        _CurrentHitsBuffer.Clear();
        foreach (var simpleRayCastable in _SimpleRayCastables)
        {
            var layer = simpleRayCastable.gameObject.layer;
            if(mask.HasValue && mask.Value.value != layer) continue;
            if (simpleRayCastable.CheckIntersection(ray, out var hitPosition) )
            {
                _CurrentHitsBuffer.Add(new SimpleRaycastHit(ray, simpleRayCastable, hitPosition));
            }
        }
        if (_CurrentHitsBuffer.Count == 0)
        {
            hits = null;
            return false;
        }
        hits = _CurrentHitsBuffer.OrderBy(hit => hit.distance);
        return true;
    }
    public static void Register (SimpleRayCastable simpleRayCastable)
    {
        _SimpleRayCastables.Add(simpleRayCastable);
    }
    public static void OnRelease (SimpleRayCastable simpleRayCastable)
    {
        _SimpleRayCastables.Remove(simpleRayCastable);
    }
}
