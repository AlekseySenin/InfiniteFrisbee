using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimpleTransformTweenActor : TweenActor
{
    
    public bool Act(TweenData data)
    {
        var tweenData = (SimpleTransformTweenData)data;
        var valueTime = Mathf.Clamp(tweenData.timeLeft / tweenData.time, 0, 1);

        var value = Vector3.Lerp(tweenData.startValue + tweenData.finalValue, tweenData.startValue, valueTime);
        switch (tweenData.type)
        {
            case TweenType.movement:
                tweenData.target.position = value;
                break;
            case TweenType.rotation:
                tweenData.target.rotation = Quaternion.Euler(value);
                break;

            default:
                break;
        }
        tweenData.timeLeft -= Time.deltaTime;

        return valueTime > 0;
    }

}
