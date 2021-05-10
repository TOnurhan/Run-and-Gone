using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static event Action<PointerEventData> PointerDragged;
    public static event Action<PointerEventData> PointerDowned;
    public static event Action<PointerEventData> PointerUpped;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        PointerDowned?.Invoke(pointerEventData);
    }

    public void OnDrag(PointerEventData pointerEventData)
    {
        PointerDragged?.Invoke(pointerEventData);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        PointerUpped?.Invoke(pointerEventData);
    }
}
