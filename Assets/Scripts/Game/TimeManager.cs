using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float slowTimeSpeed;

    void Start()
    {
        ProjectileTouchControl.OnTouchEnd += NormalTime;
        ProjectileScript.OnThrow += (x) => NormalTime();
        ProjectileScript.OnStop += NormalTime;
        ProjectileScript.OnDraw += SlowDown;
    }

    private void OnDestroy()
    {
        ProjectileTouchControl.OnTouchEnd -= NormalTime;
        ProjectileScript.OnThrow -= (x) => NormalTime();
        ProjectileScript.OnStop -= NormalTime;
        ProjectileScript.OnDraw -= SlowDown;
    }

    private void SlowDown()
    {
        Time.timeScale = slowTimeSpeed;
    } 

    private void NormalTime()
    {
        Time.timeScale = 1;
    }
}
