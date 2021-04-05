using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] PlayerChar playerChar;
    public Animator animator;

    private void Start()
    {
        playerChar.OnMove += Animate;
        GameController.OnGameWin += Win;
    }

    private void Animate(MovementType movementType)
    {
        switch (movementType)
        {
            case MovementType.Catch :
                Cach();
                break;
            case MovementType.StartThrowing:
                StartThrowing();
                break;
            default:
                break;
        }
    }

    public void StartThrowing()
    {
        animator.SetBool("Throw", true);
    }

    void Win()
    {
        animator.SetBool("Win", true);
    }

    public void Cach()
    {
        animator.SetBool("Cach", true);
    }

    private void OnDestroy()
    {
        GameController.OnGameWin -= Win;
        playerChar.OnMove -= Animate;

    }
}
