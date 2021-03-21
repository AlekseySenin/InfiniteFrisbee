using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public static GameStarter Instance;
    private int levelIndex;
    private float loadProgress;
    private bool loading = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        StartGame();
    }

    private void IsStartingScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void StartScene(int index)
    {
        Debug.Log(index);
        levelIndex = index;
        IsStartingScene(index);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SaveSystem.LoadData());
    }
}
