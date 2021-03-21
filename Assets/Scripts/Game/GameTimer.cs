using System.Collections;
using System;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;
    public bool timerStarted { get; private set; }
    public float startDellay;
    public float timeToComplite = 20;
    public float timeLeft;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        timeLeft = timeToComplite;
        if (GameController.IsTimedGame)
        {
            Invoke("StartTicking", startDellay);
        }
    }


    void StartTicking()
    {
        timerStarted = true;
        StartCoroutine(OneSecondTick());
    }

    IEnumerator OneSecondTick()
    {
        yield return null;
        timeLeft -=( Time.deltaTime*Time.timeScale);
        if (timeLeft > 0 && !ProjectileScript.Instance.LevelFiniched)
        {
            StartCoroutine(OneSecondTick());
        }
        else
        {
            if (!ProjectileScript.Instance.LevelFiniched)
            {
                GameController.OnGameLose.Invoke();
            }
        }
    }
}
