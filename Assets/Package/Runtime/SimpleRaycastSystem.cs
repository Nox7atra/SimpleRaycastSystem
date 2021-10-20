using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;

public static class SimpleRaycastSystem
{
    private static List<SimpleRayCastable> _SimpleRayCastables = new List<SimpleRayCastable>();
    private static List<SimpleRaycastHit> _CurrentHitsBuffer = new List<SimpleRaycastHit>();

    public static bool Raycast (Ray ray, out SimpleRaycastHit hit)
    {
        return Raycast(ray, out hit, LayerMask.NameToLayer("Default"));
    }
    public static bool Raycast (Ray ray, out SimpleRaycastHit hit, LayerMask mask)
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
    public static bool RaycastAll (Ray ray, out IEnumerable<SimpleRaycastHit> hits)
    {
        return RaycastAll(ray, out hits, LayerMask.NameToLayer("Default"));
    }
    public static bool RaycastAll (Ray ray, out IEnumerable<SimpleRaycastHit> hits, LayerMask mask)
    {
        _CurrentHitsBuffer.Clear();
        foreach (var simpleRayCastable in _SimpleRayCastables)
        {
            if (simpleRayCastable.CheckIntersection(ray, out var hitPosition))
            {
                _CurrentHitsBuffer.Add(new SimpleRaycastHit(ray, simpleRayCastable, hitPosition));
            }
        }
        if (_CurrentHitsBuffer.Count == 0)
        {
            hits = null;
            return false;
        }
        hits = _CurrentHitsBuffer.OrderBy(hit => hit.distance).Where(x=> mask == (mask | (1 << x.transform.gameObject.layer)));
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
