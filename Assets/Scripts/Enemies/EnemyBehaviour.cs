using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    
    public abstract IEnumerator Act();

    public abstract void Activate(Transform player);
    public abstract void Diactivate();


}
