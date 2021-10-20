using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class SimpleRaycaster : BaseRaycaster
{
	protected Camera m_EventCamera;
	public override Camera eventCamera
	{
		get
		{
			if (m_EventCamera == null)
				m_EventCamera = GetComponent<Camera>();
			return m_EventCamera ? m_EventCamera : Camera.main;
		}
	}
	protected bool ComputeRayAndDistance(PointerEventData eventData, ref Ray ray, ref int eventDisplayIndex, ref float distanceToClipPlane)
	{
		if (eventCamera == null)
			return false;

		var eventPosition = Display.RelativeMouseAt(eventData.position);
		if (eventPosition != Vector3.zero)
		{
			// We support multiple display and display identification based on event position.
			eventDisplayIndex = (int)eventPosition.z;

			// Discard events that are not part of this display so the user does not interact with multiple displays at once.
			if (eventDisplayIndex != eventCamera.targetDisplay)
				return false;
		}
		else
		{
			// The multiple display system is not supported on all platforms, when it is not supported the returned position
			// will be all zeros so when the returned index is 0 we will default to the event data to be safe.
			eventPosition = eventData.position;
		}

		// Cull ray casts that are outside of the view rect. (case 636595)
		if (!eventCamera.pixelRect.Contains(eventPosition))
			return false;

		ray = eventCamera.ScreenPointToRay(eventPosition);
		// compensate far plane distance - see MouseEvents.cs
		float projectionDirection = ray.direction.z;
		distanceToClipPlane = Mathf.Approximately(0.0f, projectionDirection)
			? Mathf.Infinity
			: Mathf.Abs((eventCamera.farClipPlane - eventCamera.nearClipPlane) / projectionDirection);
		return true;
	}
	public override void Raycast (PointerEventData eventData, List<RaycastResult> resultAppendList)
	{
		var ray = new Ray();
		int displayIndex = 0;
		float distanceToClipPlane = 0;
		if (!ComputeRayAndDistance(eventData, ref ray, ref displayIndex, ref distanceToClipPlane))
			return;
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
					worldPosition = raycastHit.point,
					worldNormal = Vector3.zero,
					screenPosition = eventData.position,
					displayIndex = displayIndex,
					index = resultAppendList.Count,
					sortingLayer = 0,
					sortingOrder = 0
				});
			}
		}
	}
}
