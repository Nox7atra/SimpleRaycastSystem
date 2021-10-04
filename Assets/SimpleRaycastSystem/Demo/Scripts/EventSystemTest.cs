using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemTest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter (PointerEventData eventData)
    {
        Debug.Log("Enter");
    }
    public void OnPointerExit (PointerEventData eventData)
    {
        Debug.Log("Exit");
    }
}
