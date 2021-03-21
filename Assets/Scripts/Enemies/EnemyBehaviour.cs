using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected Transform targetPlayer;
    [SerializeField] protected float speed;
    [SerializeField] protected float curantSpeed;
    [SerializeField] protected float speedBlending = 0.1f;
    [SerializeField] protected float patroolDistance;
    private float curantDistance;
    [SerializeField] protected bool moveRight;

    [SerializeField] protected float jumpSpeed;
    [SerializeField] protected float jumpDistance;
    [SerializeField] protected bool isStanding;
    [SerializeField] protected EnemyAnimationManager animationManager;


    public static Action OnWin;
    protected bool isJumping;
    public Vector3 move { get; protected set; }
    
    public abstract IEnumerator Act();

    protected void Start()
    {
        GameController.OnGameLose += Diactivate;
    }

    public void Activate(Transform player)
    {
        targetPlayer = player;
        if (!isStanding)
        {
            StartCoroutine(Act());
        }
    }

    public virtual void Diactivate()
    {
        
        StopAllCoroutines();
        animationManager.Stop();
    }

    public virtual void Patrool()
    {

        float derectionSpeed = moveRight ? speed *Time.deltaTime : Time.deltaTime * speed * -1;
        if ((derectionSpeed >0 && curantSpeed< derectionSpeed)|| (derectionSpeed < 0 && curantSpeed > derectionSpeed))
        {
            curantSpeed += derectionSpeed * speedBlending;
        }
        animationManager.Move(new Vector2(0, curantSpeed / (speed * Time.deltaTime)));
        curantDistance += Mathf.Abs(derectionSpeed);
        transform.position += transform.right * derectionSpeed;
        if (curantDistance >= patroolDistance)
        {
            curantDistance = 0;
            moveRight = !moveRight;
        }

       
    }

    public void Jump(Vector3 destanation)
    {
        if (!isJumping)
        {
            isJumping = true;
            StopAllCoroutines();
            Vector3 path = destanation - transform.position;
            transform.forward = new Vector3(path.normalized.x, 0, path.normalized.z);
            StartCoroutine(Fly());
            animationManager.Jump();
        }
    }

    protected IEnumerator Fly()
    {
        yield return null;
        transform.position += transform.forward * jumpSpeed * Time.deltaTime;
        jumpDistance -= jumpSpeed * Time.deltaTime;
        if (jumpDistance > 0)
        {
            StartCoroutine(Fly());
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        GameController.OnGameLose -= Diactivate;
    }
}
