using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        BGM1,
        BGM2,
        BGM3,
        Blast,
        Blast2,
        Click,
        Expand,
        MainMenuBGM,
        MapSelectBGM,
        MapSelectBGM2,
        Boop,
        Puke,
        TimeStart,
        TimeEnd,
        WinBGM,
        PlayerMove,
        PickUp,
        CloneOn,
        Cloneoff
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject winBGMGameObject;
    private static HashSet<Sound> playedOneTimeSounds; // To track sounds that have been played once

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
        playedOneTimeSounds = new HashSet<Sound>(); // Initialize the set
    }

    public static AudioSource PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = null;

            if (sound == Sound.WinBGM)
            {
                // Check if WinBGM GameObject already exists
                if (winBGMGameObject == null)
                {
                    winBGMGameObject = new GameObject("WinBGM");
                    soundGameObject = winBGMGameObject;
                }
                else
                {
                    // WinBGM GameObject already exists, no need to create a new one
                    return null;
                }
            }
            else
            {
                // Create a new GameObject for other sounds
                soundGameObject = new GameObject("Sound");
            }

            var soundType = GetSoundType(sound);
            soundGameObject.tag = soundType == GameAssets.SoundType.BGM ? "BGM" : "SoundEffect";

            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.volume = soundType == GameAssets.SoundType.BGM ? PlayerPrefs.GetFloat("BGMVolume", 1f) : PlayerPrefs.GetFloat("SFXVolume", 1f);
            DestroySound destroySound = soundGameObject.AddComponent<DestroySound>();
            destroySound.delay = 20f;
            audioSource.PlayOneShot(GetAudioClip(sound));

            return audioSource;
        }
        return null;
    }


    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .3f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioclip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioclip.sound == sound)
            {
                return soundAudioclip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    private static GameAssets.SoundType GetSoundType(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioclip in GameAssets.i.soundAudioClipArray)
        {
            if (soundAudioclip.sound == sound)
            {
                return soundAudioclip.soundType;
            }
        }
        Debug.LogError("Sound type for " + sound + " not found!");
        return GameAssets.SoundType.SFX;
    }
}
