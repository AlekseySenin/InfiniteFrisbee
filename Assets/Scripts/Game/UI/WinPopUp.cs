using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPopUp : PopUp
{
    [SerializeField] private Button replayBtn;
    [SerializeField] private Button startNextBtn;
    [SerializeField] private int nextLevelIndex;


    private void Start()
    {
        replayBtn.onClick.AddListener(Replay);

        startNextBtn.onClick.AddListener(StartNext);
    }

    void Replay()
    {
        GameController.gameRuned++;
        SaveSystem.SaveData(SceneManager.GetActiveScene().buildIndex);
        GameStarter.Instance.StartScene(SceneManager.GetActiveScene().buildIndex);

    }

    void StartNext()
    {

        GameController.gameRuned++;
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            SaveSystem.SaveData(SceneManager.GetActiveScene().buildIndex + 1);
            GameStarter.Instance.StartScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else 
        {
            SaveSystem.SaveData(SceneManager.GetActiveScene().buildIndex);
            GameStarter.Instance.StartScene( 1);
        }
    }
}
