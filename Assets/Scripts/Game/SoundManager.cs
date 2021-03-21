using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip song;
    private List <AudioSource> sources;
    [SerializeField] private List <Sound> sounds;
    [SerializeField] private AudioSource musicPlayer;
    public static OptionsData OptionsData;
    public bool musicOn;
    public bool soundOn;

    public static SoundManager Instance;

    private void Awake()
    {
        
        OptionsData = SaveSystem.LoadOptions();
        
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
            if (musicPlayer != null)
            {
                musicPlayer.loop = true;
            }
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        PlayMusic();
    }

    public static void PlayMusic()
    {
        if (Instance != null)
        if (Instance.musicOn && Instance.song != null)
        {
                Instance.musicPlayer.volume = OptionsData.musicVolume;
                Instance.musicPlayer.clip = Instance.song;
                Instance.musicPlayer.Play();
        }
    }

    public void SetSound(float vol)
    {
        OptionsData.soundVolume = vol;
    }

    public void SetMusic(float vol)
    {
        OptionsData.musicVolume = vol;
        musicPlayer.volume = vol;
    }

    public static void PlaySound(AudioSource source, SoundType soundType)
    {
        if(Instance != null)
        if (Instance.soundOn)
        {
            Sound sound = Instance.sounds.Find(x => x.type == soundType);
            if (sound != null)
            {
                source.PlayOneShot(sound.clip, OptionsData.soundVolume);
            }
        }
    }

}

[System.Serializable]
public enum SoundType
{
    win, lose, throwBall, cachBall, enemyCach
}

[System.Serializable]
public class Sound 
{
    public AudioClip clip;
    public SoundType type;
}