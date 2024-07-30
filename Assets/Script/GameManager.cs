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

    

    void Start()
    {
        

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
        }
    }

    
}
