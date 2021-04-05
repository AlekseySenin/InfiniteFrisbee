using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != GameController.picher.gameObject && other.gameObject != GameController.cacher.gameObject)
        {
            GameController.OnGameLose?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject == GameController.picher.gameObject || !collision.gameObject == GameController.cacher.gameObject)
        {;
            GameController.OnGameLose?.Invoke();
        }
    }
}
