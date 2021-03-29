using System;
using System.Collections;
using UnityEngine;

public abstract class Tween : MonoBehaviour
{
   // protected  TweenData  _tweenData;
    protected float _deltaTime { get { return Time.deltaTime; } }
    protected TweenActor _tweenActor;
    public virtual void StartTween(TweenData tweenData)
    {
        tweenData.timeLeft = tweenData.time;
        StartCoroutine(OnDoTween(tweenData));
    }

    protected virtual IEnumerator OnDoTween(TweenData tweenData)
    {
        yield return null;
        tweenData.timeLeft -= _deltaTime;
        DoTween(tweenData,_tweenActor);
        
    }

    protected virtual void DoTween(TweenData tweenData, TweenActor tweenActor)
    {
        bool tweenContinues = tweenActor.Act(tweenData);
        if (tweenContinues)// убрать deltatime!
        {
            StartCoroutine(OnDoTween(tweenData));
        }
        else
        {
            OnTweenFinished?.Invoke(tweenData);
        }
    }

    protected Action <TweenData> OnTweenFinished;
    public abstract void Init();
}
