using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenControlTweenSequence : TweenSequence
{
    [SerializeField] List<TweenControlTweenData> controlTweenDatas;

    private void Awake()
    {
        _tweenDatas = new List<TweenData>(controlTweenDatas);
    }

    protected override IEnumerator OnDoTween(TweenData tweenData)
    {
        if(tweenData.time<=0) 
        {
            ActivateTween(tweenData);
        }
        yield return new WaitForSeconds(tweenData.time);
        if (tweenData.time > 0)
        {
            ActivateTween(tweenData);
        }
    }

    private void ActivateTween(TweenData tweenData)
    {
        TweenControlTweenData data = (TweenControlTweenData)tweenData;
        data.target.Init();
        StartNextTween(tweenData);
    }
}
