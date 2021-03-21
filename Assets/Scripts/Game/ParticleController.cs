using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particleSystems;
    void Start()
    {
        GameController.OnGameWin += ActivateParticles;
        foreach (var item in particleSystems)
        {
            item.Stop();
        }
    }

    private void OnDestroy()
    {
        GameController.OnGameWin -= ActivateParticles;

    }

    private void ActivateParticles()
    {
        foreach (var item in particleSystems)
        {
            item.Play();
        }
    }
}
