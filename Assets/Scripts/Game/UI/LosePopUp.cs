using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using BizzyBeeGames.MobileAds;

public class LosePopUp : PopUp
{
    [SerializeField] private Button replayBtn;

    private void Start()
    {
        replayBtn.onClick.AddListener(Replay);

    }

    void Replay()
    {
        GameController.gameRuned++;
        SaveSystem.SaveData(SceneManager.GetActiveScene().buildIndex);
        Debug.Log((SceneManager.GetActiveScene().buildIndex));
        GameStarter.Instance.StartScene(SceneManager.GetActiveScene().buildIndex);

    }

  
}
