using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStartBehaviour : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Update()
    {
        if (slider.value < 0.95f)
        {
            slider.value += 0.01f;
        }
        else
        {
            GameStarter.Instance.StartGame();
            Destroy(gameObject);
        }
    }
}
