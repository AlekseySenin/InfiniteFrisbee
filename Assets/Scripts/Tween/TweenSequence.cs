using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TweenSequence : Tween
{
    protected List<TweenData> _tweenDatas;
    [SerializeField] private bool startOnAwake = true;
    [SerializeField] private bool looping;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        if (startOnAwake)
        {
            Init();
        }
        OnTweenFinished = StartNextTween;
    }

    public override void Init()
    {
        if (_tweenDatas.Count > 0)
        {
            StartTween(_tweenDatas[0]);
        } 
    }

    protected virtual void StartNextTween(TweenData tweenData)
    {
        int index = _tweenDatas.FindIndex(x=> x == tweenData);
        if(index < _tweenDatas.Count - 1)
        {
            StartTweenAtIndex(index + 1);
        }
        else if (looping)
        {
            StartTweenAtIndex(0);
        }        
    }

    private void StartTweenAtIndex(int index)
    {
        StartTween(_tweenDatas[index]);
    }
}
