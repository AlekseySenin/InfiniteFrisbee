using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : EnemyBehaviour
{
    [SerializeField] float closeDistance;

    public override IEnumerator Act()
    {
        yield return null;
        Vector3 path = targetPlayer.transform.position - transform.position;
        if (path.magnitude > closeDistance)
        {
            transform.forward = new Vector3(path.normalized.x, 0, path.normalized.z);
            transform.position += transform.forward * (speed * Time.deltaTime);
            animationManager.Move(new Vector2(1, 0));
        }
        else
        {
            Patrool();
        }
        StartCoroutine(Act());
    }
    public override void Patrool()
    {
        Vector3 path = targetPlayer.transform.position - transform.position;
        transform.forward = new Vector3(path.normalized.x, 0, path.normalized.z);
        base.Patrool();
    }
}
