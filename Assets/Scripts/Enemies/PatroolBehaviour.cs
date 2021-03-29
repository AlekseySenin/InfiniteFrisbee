using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroolBehaviour : OldEnemyBehaviour
{
    public override IEnumerator Act()
    {
        yield return null;
        Patrool();
        StartCoroutine(Act());
    }


    public override void Diactivate()
    {
        base.Diactivate();
    }
}
