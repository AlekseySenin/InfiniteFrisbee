using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTransformTweenSequence : TweenSequence
{
    [SerializeField] private Transform targetTransform;
    public List <SimpleTransformTweenData> simpleTransformTweenDatas;

    private void Awake()
    {
        _tweenActor = new SimpleTransformTweenActor();
        if (targetTransform == null)
        {
            targetTransform = transform;
        }
        _tweenDatas = new List<TweenData>(simpleTransformTweenDatas);
    }


    public override void StartTween(TweenData tweenData)
    {
        SimpleTransformTweenData stTweenData = (SimpleTransformTweenData)tweenData;
        stTweenData.target = targetTransform;
        switch (stTweenData.type)
        {
            case TweenType.movement:
                stTweenData.startValue = targetTransform.position;
                break;
            case TweenType.rotation:
                stTweenData.startValue = targetTransform.rotation.eulerAngles;
                break;
            case TweenType.scale:
                stTweenData.startValue = targetTransform.lossyScale;
                break;
            default:
                break;
        }
        base.StartTween(tweenData);
    }

}
