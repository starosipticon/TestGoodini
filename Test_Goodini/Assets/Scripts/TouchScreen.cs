using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float touchDist = 0;
    private float lastDist = 0;
    public float horizontalRotation { get; private set; } = 0f;
    public float verticalRotation { get; private set; } = 0f;
    public float deltaDistance { get; private set; } = 0;
    public bool isUsed { get; private set; } = false;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            horizontalRotation += touch.deltaPosition.x  * 0.1f * Time.deltaTime;
            verticalRotation += touch.deltaPosition.y  * 0.1f * Time.deltaTime;

            horizontalRotation = Mathf.Clamp(horizontalRotation, -1f, 1f);
            verticalRotation = Mathf.Clamp(verticalRotation, -1f, 1f);

            if (touch.phase == TouchPhase.Ended)
            {
                horizontalRotation = 0;
                verticalRotation = 0;
            }
        }

        if (Input.touchCount == 2)
        {
            GetPinchDistance();
        }
    }

    private void GetPinchDistance()
    {
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
        {
            lastDist = Vector2.Distance(touch1.position, touch2.position);
        }

        if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
        {
            float newDist = Vector2.Distance(touch1.position, touch2.position);
            touchDist = lastDist - newDist;
            deltaDistance = newDist - lastDist;
            lastDist = newDist;
        }

        if (touch1.phase == TouchPhase.Ended && touch2.phase == TouchPhase.Ended)
        {
            deltaDistance = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isUsed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isUsed = false;
    }
}
