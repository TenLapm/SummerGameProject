using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GridManager gridManager;
    private Timer timer;
    [HideInInspector] public bool playerAWin = false;
    [HideInInspector] public bool playerBWin = false;
    [HideInInspector] public bool GameEnded = false;

    private AudioSource bgmAudioSource;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            bgmAudioSource = SoundManager.PlaySound(SoundManager.Sound.BGM1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            bgmAudioSource = SoundManager.PlaySound(SoundManager.Sound.BGM2);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            bgmAudioSource = SoundManager.PlaySound(SoundManager.Sound.BGM3);
        }

        gridManager = FindObjectOfType<GridManager>();
        timer = FindObjectOfType<Timer>();
    }

    void FixedUpdate()
    {
        if (timer.CurrentTime <= 0 && !GameEnded)
        {
            if (gridManager.PlayerAPercent > gridManager.PlayerBPercent)
            {
                playerAWin = true;
            }
            else if (gridManager.PlayerBPercent > gridManager.PlayerAPercent)
            {
                playerBWin = true;
            }
            else
            {
                Application.Quit();
            }
            GameEnded = true;
            StopBGM();
        }
    }

    private void StopBGM()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
            Destroy(bgmAudioSource.gameObject);
        }
    }
}
