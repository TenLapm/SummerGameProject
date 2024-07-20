using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Map1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MapSelect()
    {
        SceneManager.LoadScene("MapSelect");
    }
}
