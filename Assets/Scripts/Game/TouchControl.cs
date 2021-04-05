using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    private Vector2 touchStart;
    private float offset;
    public static Action OnAimStart;
    [SerializeField] private ProjectileScript projectile;
    [SerializeField] private float scale;
    public static bool screenPressed;

    public static Action <float> OnTouchMoved;
    public static Action<float> OnTouchEnded;


    // Update is called once per frame
    void Update()
    {
        GameController.canThrow = !ProjectileScript.isMoveing;

        DetectTouch();
    }

    private void DetectTouch()
    {

        if (Input.touchCount > 0 && GameController.canThrow)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnAimStart?.Invoke();
                    screenPressed = true;
                    touchStart = touch.position;
                    offset = 0;
                    OnTouchMoved?.Invoke(offset);
                    break;
                case TouchPhase.Moved:
                    screenPressed = true;
                    offset = (touch.position.x - touchStart.x) / scale;
                    OnTouchMoved?.Invoke(offset);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    GameController.picher.ThrowProjectile();
                    GameController.offset = offset;
                    offset = 0;
                    OnTouchEnded?.Invoke(offset);
                    screenPressed = false;

                    break;
                case TouchPhase.Canceled:
                    GameController.picher.ThrowProjectile();
                    GameController.offset = offset;
                    offset = 0;
                    OnTouchEnded?.Invoke(offset);
                    screenPressed = false;
                    break;
                default:
                    break;
            }

        }

        

        else if (GameController.canThrow)
        {
            
            if (Input.GetMouseButtonDown(0))
            {

                    screenPressed = true;
                    OnAimStart?.Invoke();
                    touchStart = Input.mousePosition;
                    offset = 0;

            }
            if (Input.GetMouseButtonUp(0))
            {
                
                if (screenPressed )
                {
                    GameController.picher.ThrowProjectile();
                    OnTouchEnded?.Invoke(offset);
                }
                
                GameController.offset = offset;
                screenPressed = false;
                offset = 0;
            }


            if (screenPressed)
            {
                offset = (Input.mousePosition.x - touchStart.x) / scale;
                OnTouchMoved?.Invoke(offset);
            }

        }
    }
}
