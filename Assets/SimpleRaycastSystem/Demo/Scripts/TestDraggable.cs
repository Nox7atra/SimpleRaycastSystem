using UnityEngine;
using UnityEngine.EventSystems;

public class TestDraggable : MonoBehaviour, IDragHandler, IBeginDragHandler
{
	private Vector3 _Delta;
	private Camera _Camera;
	private void Awake ()
	{
		_Camera = Camera.main;
	}
	public void OnDrag (PointerEventData eventData)
	{
		Ray ray = _Camera.ScreenPointToRay(eventData.position);
		var pos = transform.position;
		Vector3 rayPoint = ray.GetPoint(Vector3.Distance(pos, _Camera.transform.position));
		var z = pos.z;
	    pos = rayPoint + _Delta;
	    pos.z = z;
	    transform.position = pos;
    }
    public void OnBeginDrag (PointerEventData eventData)
    {
	    Ray ray = _Camera.ScreenPointToRay(eventData.position);
	    var pos = transform.position;
	    Vector3 rayPoint = ray.GetPoint(Vector3.Distance(pos, _Camera.transform.position));
	    _Delta = pos - rayPoint;
    }
}
