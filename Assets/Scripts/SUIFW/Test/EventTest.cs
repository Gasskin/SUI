using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTest : EventTrigger
{
    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Debug.Log("down");
    }

    public override void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Debug.Log("click");
    }
}
