using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleTransformRaycaster : BaseRaycaster
{
    public override Camera eventCamera {
        get;
    }
    public override void Raycast (PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        var tr = transform;
        var ray = new Ray(tr.position, tr.forward);
        
        IEnumerable<SimpleRaycastHit> hits;
        SimpleRaycastSystem.RaycastAll(ray, out hits);
        if (hits != null)
        {
            foreach (var raycastHit in hits)
            {
                resultAppendList.Add(new RaycastResult()
                {
                    gameObject = raycastHit.collider.gameObject,
                    module = this,
                    distance = raycastHit.distance,
                    worldPosition = Vector3.zero,
                    worldNormal = Vector3.zero,
                    screenPosition = eventData.position,
                    displayIndex = -1,
                    index = resultAppendList.Count,
                    sortingLayer = 0,
                    sortingOrder = 0
                });
            }
        }
    }
    private void OnDrawGizmos ()
    {
        var tr = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(tr.position, tr.forward * 1000);
    }
}
