using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SoundManager.PlaySound(SoundManager.Sound.MainMenuBGM);
        }else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SoundManager.PlaySound(SoundManager.Sound.MapSelectBGM);
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
}
