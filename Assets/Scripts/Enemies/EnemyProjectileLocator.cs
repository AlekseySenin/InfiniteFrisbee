using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileLocator : MonoBehaviour
{
    [SerializeField] protected LayerMask hittableLayers = -1;
    [SerializeField] protected float fovRadius;
    [SerializeField] OldEnemyBehaviour enemyBehaviour;
    public Transform hand;
    private bool projectileInRange;

    private void Start()
    {
        if (enemyBehaviour == null)
        {
            enemyBehaviour = GetComponent<OldEnemyBehaviour>();
        }
    }

    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, fovRadius, transform.position, 0, hittableLayers, QueryTriggerInteraction.UseGlobal);
        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.TryGetComponent(out ProjectileScript proj)&& !ProjectileScript.suupershot)
            {
                if (!projectileInRange)
                {
                    projectileInRange = true;
                    ProjectileScript.EnemiesInRange.Add(gameObject);
                }
                ProjectileScript.AddFire(0.1f);

                enemyBehaviour.Jump(hit.transform.position);
            }
            else if (projectileInRange)
            {
                projectileInRange = false;
                ProjectileScript.EnemiesInRange.Remove(gameObject);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fovRadius);
    }
}
