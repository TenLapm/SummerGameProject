using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject SettingPanel;

    private void Start()
    {
        SettingPanel.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SoundManager.PlaySound(SoundManager.Sound.MainMenuBGM);
        } else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                SoundManager.PlaySound(SoundManager.Sound.MapSelectBGM);
            }
            else
            {
                SoundManager.PlaySound(SoundManager.Sound.MapSelectBGM2);
            }
            
        }

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void Map1()
    {
        SceneManager.LoadScene(2);
    }
    public void Map2()
    {
        SceneManager.LoadScene(3);
    }
    public void Map3()
    {
        SceneManager.LoadScene(4);
    }
    public void Mapa3()
    {
        SceneManager.LoadScene("Mapa3");
    }
    public void SettingOn()
    {
        SettingPanel.SetActive(true);
    }
    public void SettingOff()
    {
        SettingPanel.SetActive(false);
    }
}
