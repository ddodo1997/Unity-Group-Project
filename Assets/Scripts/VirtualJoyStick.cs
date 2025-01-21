using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform background;
    public RectTransform handle;

    private float joystickRadius;

    public Vector2 Input { get; private set; }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out Vector2 point))
        {
            point = Vector2.ClampMagnitude(point, joystickRadius);
            handle.anchoredPosition = point;

            Input = point / joystickRadius;
        }
    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Input = Vector2.zero;
        handle.anchoredPosition = Input;
    }

    private void Start()
    {
        joystickRadius = background.rect.width * 0.5f;
    }
}
