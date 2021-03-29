using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenControlTweenSequence : TweenSequence
{
    [SerializeField] List<TweenControlTweenData> controlTweenDatas;

    protected override void Start()
    {

        _tweenDatas = new List<TweenData>(controlTweenDatas);
        base.Start();
    }

    protected override IEnumerator OnDoTween(TweenData tweenData)
    {
        yield return new WaitForSeconds(tweenData.time);
        TweenControlTweenData data = (TweenControlTweenData)tweenData;
        data.target.Init();
        print(data.target.gameObject.name);
        StartNextTween(tweenData);
    }
}
