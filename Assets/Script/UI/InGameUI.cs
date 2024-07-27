using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//using UnityEditor.Animations;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject Interface;
    
    [SerializeField] private TMP_Text timerText;
    private Timer timer;
    private bool paused = false;
    private GameManager gameManager;

    [SerializeField] private GameObject PlayerAWin;
    [SerializeField] private GameObject PlayerBWin;

    private bool donefade;


    void Start()
    {

        Interface.SetActive(true);
        timer = GetComponent<Timer>();
        gameManager = FindObjectOfType<GameManager>();
        donefade = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Resume();
        }

        if (timer.CurrentTime < 0)
        {
            timerText.text = "0";
        }
        else if (timer.CurrentTime >= 60)
        {
            int minutes = Mathf.FloorToInt(timer.CurrentTime / 60);
            int seconds = Mathf.FloorToInt(timer.CurrentTime % 60);
            timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = string.Format("{0:0}", Mathf.FloorToInt(timer.CurrentTime));
        }
        if (gameManager.GameEnded)
        {
            GameEnd();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        PauseMenu.SetActive(true);
        Interface.SetActive(false);
        paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
        Interface.SetActive(true);
        paused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        paused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MapSelect()
    {
        SceneManager.LoadScene("MapSelect");
    }

    public void GameEnd()
    {
        StartCoroutine(Transitioning(1));
        if (donefade) {
            
            PauseMenu.SetActive(false);
            Interface.SetActive(false);
            Time.timeScale = 1.0f;
            SoundManager.PlaySound(SoundManager.Sound.WinBGM);
            if (gameManager.playerAWin)
            {
                PlayerAWin.SetActive(true);
            }
            else if (gameManager.playerBWin)
            {
                PlayerBWin.SetActive(true);
            }
        }
        
    }

    private IEnumerator Transitioning(int duration)
    {
        
        yield return new WaitForSeconds(duration);
        donefade = true;
    }
}
