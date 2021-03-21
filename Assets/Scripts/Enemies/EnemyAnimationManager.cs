using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    private void Start()
    {
        GameController.OnGameLose += Win;
    }

    public void Move(Vector2 derection)
    {
        animator.SetFloat("Speed", derection.magnitude);
        animator.SetFloat("Forward", derection.x);
        animator.SetFloat("Right", derection.y);
    }

    public void Stop()
    {
        Move(new Vector2());
    }

    public void Jump()
    {
        animator.SetBool("Jump", true);
    }

    public void Win()
    {
        animator.SetBool("Win", true);
    }

    private void OnDestroy()
    {
        GameController.OnGameLose -= Win;
    }
    
}
