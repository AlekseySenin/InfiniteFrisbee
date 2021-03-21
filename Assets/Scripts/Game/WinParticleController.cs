using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;

    private void Start()
    {
        GameController.OnGameWin += Win;
    }

    private void Win()
    {
        particles.Play();
    }

    private void OnDestroy()
    {
        GameController.OnGameWin -= Win;
    }
}
