using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IEndDragHandler, IBeginDragHandler,IDragHandler
{
    public Image Stick;
    public Image groundStick;

    public PlayerController PC;

    private Vector2 startPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        groundStick.gameObject.SetActive(true);
        startPosition = eventData.position;
        groundStick.transform.position = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 nowPosition = eventData.position;
        Vector2 direction = startPosition - nowPosition;
        Vector2 normDirection = direction;

        normDirection.Normalize();
        float magnitude = Mathf.Clamp(direction.magnitude, -50, 50);

        Stick.transform.position = startPosition - normDirection *magnitude ;

        if (Mathf.Abs(magnitude) > 20)
            PC.direction = -normDirection * magnitude / 50;
        else
            PC.direction = Vector2.zero;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        groundStick.gameObject.SetActive(false);
        //Stick.transform.position = eventData.position;
        PC.direction = Vector2.zero;
    }


}
