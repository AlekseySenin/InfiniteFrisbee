using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverWindow : MonoBehaviour
{
    [SerializeField] private GameObject PopupWindow;
    [SerializeField] private Button restartGameButton;

    void Start()
    {
        restartGameButton.onClick.AddListener(RestartGame);
        GameController.OnGameLose += ShowPopUp;

    }

    private void OnDestroy()
    {
        GameController.OnGameLose -= ShowPopUp;

    }

    private void ShowPopUp()
    {
        PopupWindow.SetActive(true);
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
   
}
