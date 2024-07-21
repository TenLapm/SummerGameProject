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
        SoundManager.PlaySound(SoundManager.Sound.Back);
        SoundManager.PlaySound(SoundManager.Sound.TestSound);
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
