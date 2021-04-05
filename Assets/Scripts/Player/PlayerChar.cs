using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChar : MonoBehaviour
{
    public PlayerChar nextPlayer;
   // [SerializeField] private List<EnemyBehaviour> enemyes;
    public Transform hand;
    public ProjectileScript projectile;

    [SerializeField] private float turnSpeed;
    [SerializeField] private float fovRadius = 1;
    [SerializeField] private float cahhcooldown = 0.5f;
    [SerializeField] protected LayerMask hittableLayers = -1;
    private Vector3 startForward;
    private Vector3 finishForward;
    private float turnState = 0;
    public Action<MovementType> OnMove;
    public Action OnGetBall;
    public static Action OnSegmentPassed;


    private void Start()
    {
        GameController.playerChars.Add(this);
        StartCoroutine(FollowTheProjectile());
    }

    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, fovRadius, transform.position, 0, hittableLayers, QueryTriggerInteraction.UseGlobal);
        foreach (var hit in hits)
        {
            if (hit.transform.gameObject.TryGetComponent(out ProjectileScript proj))
            {
                OnMove?.Invoke(MovementType.Catch );
            }
        }
    }

    IEnumerator FollowTheProjectile()
    {
        yield return null;
        if (projectile == null)
        {
            LookAtObject(ProjectileScript.Instance.transform);
            StartCoroutine(FollowTheProjectile());
        }
        else
        {
            StartTurningToCacher();
        }
    }

    void StartTurningToCacher()
    {
        startForward = transform.forward;
        finishForward = ( nextPlayer.transform.position - transform.position).normalized;
        StartCoroutine(TurnToCacher());
    }

    private void LookAtObject(Transform obj)
    {
        Vector3 newForward = (obj.position - transform.position );
       
        transform.forward = new Vector3(0,0, newForward.z);
    }


    public void ThrowProjectile()
    {
        StartCoroutine(Throw());
    } 

    IEnumerator Throw()
    {
        // projectile.HideDots();

        OnMove?.Invoke(MovementType.StartThrowing);
        yield return new WaitForSeconds(0.3f);
        if (projectile != null)
        {
            projectile.ThrowProjectile(GameController.offset);
        }
    }

    IEnumerator TurnToCacher()
    {
        yield return null;
        if (turnState < 1)
        {
            turnState += turnSpeed * Time.deltaTime;
            transform.forward = Vector3.Lerp(startForward, finishForward, turnState);
            StartCoroutine(TurnToCacher());
        }
    }

    public void Cach()
    {
        OnMove?.Invoke(MovementType.Catch );
        OnGetBall?.Invoke();
        OnSegmentPassed?.Invoke();
 
        StartCoroutine(GetReady());
    }

    IEnumerator GetReady()
    {
        yield return new WaitForSeconds(cahhcooldown);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        GameController.playerChars.Remove(this);
    }
}

public enum MovementType{
    Catch , StartThrowing
}