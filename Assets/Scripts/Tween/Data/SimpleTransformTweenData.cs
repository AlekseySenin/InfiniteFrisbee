using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimpleTransformTweenData : TweenData
{
    public Transform target { get; set; }
    public TweenType type;
    public Vector3 startValue;
    public Vector3 finalValue;
}

[System.Serializable]
public enum TweenType
{
    movement, rotation,scale
}