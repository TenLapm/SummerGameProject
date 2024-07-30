using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    
    [SerializeField] private TMP_Text timerText;
    private Timer timer;
    private bool paused = false;
    private GameManager gameManager;

    [SerializeField] private GameObject PlayerAWin;
    [SerializeField] private GameObject PlayerBWin;

    private bool donefade;

    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject countdownPanel;
    
    [HideInInspector] public bool countdownActive = false;

    private AudioSource bgmAudioSource;

    [SerializeField] private GameObject on57;
    private bool is57= false;
    void Start()
    {
        Button[] buttons = Object.FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            if (button.gameObject.GetComponent<ButtonHoverSound>() == null)
            {
                button.gameObject.AddComponent<ButtonHoverSound>();
            }
        }
        SoundManager.PlaySound(SoundManager.Sound.TimeStart);
        PauseMenu.SetActive(false);
        on57.SetActive(false);
        timer = GetComponent<Timer>();
        gameManager = FindObjectOfType<GameManager>();
        donefade = false;
        PlayerAWin.SetActive(false);
        PlayerBWin.SetActive(false);

        if (!countdownActive)
        {
            StartCoroutine(StartCountdown());
        }
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.PlaySound(SoundManager.Sound.Click);
        }
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
        if(timer.CurrentTime < 11)
        {
            on57.SetActive(true);
        }
    }

    public void Pause()
    {
        if(countdownActive == false)
        {
            Time.timeScale = 0.0f;
            PauseMenu.SetActive(true);
            paused = true;
        }
        
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
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

        StopBGM();
        if (!countdownActive)
        {
            StartCoroutine(Transitioning(1));
            if (donefade)
            {
                PauseMenu.SetActive(false);
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
    }
    private void StopBGM()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
            Destroy(bgmAudioSource.gameObject);
        }
    }

    private IEnumerator Transitioning(int duration)
    {
        
        yield return new WaitForSeconds(duration);
        donefade = true;
    }

    private IEnumerator StartCountdown()
    {
        countdownActive = true;
        countdownPanel.SetActive(true);
        

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1f);
        countdownPanel.SetActive(false);
        countdownText.text = "";
        countdownActive = false;
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
    }
}
