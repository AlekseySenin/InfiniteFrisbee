using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTouchControl : MonoBehaviour
{
    private Vector2 touchStart;
    private float offset;
    public static Action OnAimStart;
    [SerializeField] private ProjectileScript projectile;
    [SerializeField] private float scale;
    public static Action OnTouchEnd;
    public static bool screenPressed;
    private bool gameEnded;

    private void Start()
    {
        GameController.OnGameLose += () => gameEnded = true;
        offset = 0;
    }
    // Update is called once per frame
    void Update()
    {
        GameController.canThrow = !ProjectileScript.isMoveing;

        DetectTouch();
    }

    private void DetectTouch()
    {
        if (Input.touchCount > 0 && !gameEnded && GameController.canThrow && !UIManager.OptionsWindwOpened && !ProjectileScript.suupershot)
        {


            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnAimStart?.Invoke();
                    screenPressed = true;
                    touchStart = touch.position;
                    offset = 0;
                    projectile.DrowTrajectory(offset);
                    break;
                case TouchPhase.Moved:
                    screenPressed = true;
                    offset = (touch.position.x - touchStart.x) / scale;
                    projectile.DrowTrajectory(offset);
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    if (screenPressed && (GameTimer.Instance.timerStarted || !GameController.IsTimedGame))
                    {
                        GameController.picher.ThrowProjectile();
                    }
                    GameController.offset = offset;
                    offset = 0;
                    projectile.HideDots();
                    OnTouchEnd?.Invoke();
                    screenPressed = false;

                    break;
                case TouchPhase.Canceled:
                    if (screenPressed && (GameTimer.Instance.timerStarted || !GameController.IsTimedGame))
                    {
                        GameController.picher.ThrowProjectile();
                    }
                    GameController.offset = offset;
                    offset = 0;
                    projectile.HideDots();
                    OnTouchEnd?.Invoke();
                    screenPressed = false;
                    break;
                default:
                    break;
            }

        }

        else if (!gameEnded && GameController.canThrow && !UIManager.OptionsWindwOpened && !ProjectileScript.suupershot)
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (!gameEnded)
                {
                    screenPressed = true;
                    OnAimStart?.Invoke();
                    touchStart = Input.mousePosition;
                    offset = 0;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                
                if (screenPressed && (GameTimer.Instance.timerStarted || !GameController.IsTimedGame) && !gameEnded)
                {
                    GameController.picher.ThrowProjectile();
                }
                projectile.HideDots();
                OnTouchEnd?.Invoke();
                GameController.offset = offset;
                screenPressed = false;
                offset = 0;
            }


            if (screenPressed && !gameEnded)
            {
                offset = (Input.mousePosition.x - touchStart.x) / scale;
                projectile.DrowTrajectory(offset);
            }

        }

        screenPressed = UIManager.OptionsWindwOpened ? false : screenPressed;
        if (!screenPressed)
        {
            ProjectileScript.Instance.HideDots();
        }
       
    }
}
