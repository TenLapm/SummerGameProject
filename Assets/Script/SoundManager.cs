using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound
    {
        TestSound,
        PlayerMove,
        Back
    }

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");

            var soundType = GetSoundType(sound);
            soundGameObject.tag = soundType == GameAssets.SoundType.BGM ? "BGM" : "SoundEffect";

            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.volume = soundType == GameAssets.SoundType.BGM ? PlayerPrefs.GetFloat("BGMVolume", 1f) : PlayerPrefs.GetFloat("SFXVolume", 1f);
            DestroySound destroySound = soundGameObject.AddComponent<DestroySound>();
            destroySound.delay = 20f;
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
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
