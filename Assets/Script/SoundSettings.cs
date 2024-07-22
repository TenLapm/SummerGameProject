using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;

    private void Start()
    {
        bgmSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);

        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Set initial volume levels
        OnBgmVolumeChanged(bgmSlider.value);
        OnSfxVolumeChanged(sfxSlider.value);
    }

    private void OnBgmVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("BGMVolume", value);
        foreach (var bgmObject in GameObject.FindGameObjectsWithTag("BGM"))
        {
            AudioSource audioSource = bgmObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = value;
            }
        }
    }

    private void OnSfxVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        foreach (var soundObject in GameObject.FindGameObjectsWithTag("SoundEffect"))
        {
            AudioSource audioSource = soundObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = value;
            }
        }
    }
}
