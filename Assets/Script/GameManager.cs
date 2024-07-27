using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GridManager gridManager;
    private Timer timer;
    [HideInInspector] public bool playerAWin = false;
    [HideInInspector] public bool playerBWin = false;
    [HideInInspector] public bool GameEnded = false;

    void Start()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            SoundManager.PlaySound(SoundManager.Sound.BGM1);
        }
        else if (rand == 1)
        {
            SoundManager.PlaySound(SoundManager.Sound.BGM2);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.BGM3);
        }

        gridManager = FindObjectOfType<GridManager>();
        timer = FindObjectOfType<Timer>();
    }

    void FixedUpdate()
    {
        if(timer.CurrentTime <= 0)
        {
            if(gridManager.PlayerAPercent > gridManager.PlayerBPercent)
            {
                playerAWin = true;
            }else if(gridManager.PlayerBPercent > gridManager.PlayerAPercent)
            {
                playerBWin = true;
            }
            else
            {
                Application.Quit();
            }
            GameEnded = true;
        }
    }

}
