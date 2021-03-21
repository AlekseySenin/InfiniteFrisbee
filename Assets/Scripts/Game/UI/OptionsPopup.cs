using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPopup : PopUp
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Button closeButton;


    private void Awake()
    {
        soundSlider.value = SoundManager.OptionsData.soundVolume;
        musicSlider.value = SoundManager.OptionsData.musicVolume;
        soundSlider.onValueChanged.AddListener(SetSound);
        musicSlider.onValueChanged.AddListener(SetMusic);
        closeButton.onClick.AddListener(Close);
    }

    public void Show()
    {
        soundSlider.value = SoundManager.OptionsData.soundVolume;
        musicSlider.value = SoundManager.OptionsData.musicVolume;
        UIManager.OptionsWindwOpened = true;
        ProjectileTouchControl.screenPressed = false;
    }

    void Close()
    {
        SaveSystem.SaveSettings(SoundManager.OptionsData);
        UIManager.OptionsWindwOpened = false;
        Debug.Log(UIManager.OptionsWindwOpened);
        gameObject.SetActive(false);
        ProjectileTouchControl.screenPressed = false;
    }

    void SetSound(float vol)
    {
        SoundManager.Instance.SetSound(vol);
    }
    void SetMusic(float vol)
    {
        SoundManager.Instance.SetMusic(vol);
    }
}
