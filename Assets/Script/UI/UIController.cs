using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject SettingPanel;
    public GameObject Map1BG;
    public GameObject Map2BG;
    public GameObject Map3BG;

    public GameObject Map1Button;
    public GameObject Map2Button;
    public GameObject Map3Button;

    private Dictionary<GameObject, GameObject> mapButtonToBackgroundMap;
    private GameObject selectedMapButton;

    private bool settingOn = false;

    private void Start()
    {
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>(true); 
        foreach (GameObject go in allGameObjects)
        {
            go.SetActive(true);
        }
        Button[] buttons = Object.FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            if (button.gameObject.GetComponent<ButtonHoverSound>() == null)
            {
                button.gameObject.AddComponent<ButtonHoverSound>();
            }
        }
        SettingPanel.SetActive(false);
        Map1BG.SetActive(true);
        Map2BG.SetActive(false);
        Map3BG.SetActive(false);
        settingOn = false;

        mapButtonToBackgroundMap = new Dictionary<GameObject, GameObject>
        {
            { Map1Button, Map1BG },
            { Map2Button, Map2BG },
            { Map3Button, Map3BG }
        };
        

        Map1Button.GetComponent<Button>().onClick.AddListener(() => Select(Map1Button));
        Map2Button.GetComponent<Button>().onClick.AddListener(() => Select(Map2Button));
        Map3Button.GetComponent<Button>().onClick.AddListener(() => Select(Map3Button));

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SoundManager.PlaySound(SoundManager.Sound.MainMenuBGM);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
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

            int selectedBackgroundIndex = PlayerPrefs.GetInt("SelectedMapBG", -1);
            if (selectedBackgroundIndex != -1)
            {
                SetActiveBackground(selectedBackgroundIndex);
                selectedMapButton = mapButtonToBackgroundMap.ElementAt(selectedBackgroundIndex).Key;
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

    public void Credits()
    {
        SceneManager.LoadScene(5);
    }

    public void SettingOn()
    {
        settingOn = false;
        if (settingOn == false)
        {
            SettingPanel.SetActive(true);
            settingOn = true;
        }
    }

    public void SettingOff()
    {
        settingOn = true;
        if(settingOn)
        {
            SettingPanel.SetActive(false);
            settingOn = false;
        }
        
        
    }

    public void Select(GameObject selectedButton)
    {
        for (int i = 0; i < mapButtonToBackgroundMap.Count; i++)
        {
            var entry = mapButtonToBackgroundMap.ElementAt(i);
            if (selectedButton == entry.Key)
            {
                entry.Value.SetActive(true);
                PlayerPrefs.SetInt("SelectedMapBG", i);
                selectedMapButton = selectedButton; 
            }
            else
            {
                entry.Value.SetActive(false);
            }
        }
    }

    private void SetActiveBackground(int index)
    {
        for (int i = 0; i < mapButtonToBackgroundMap.Count; i++)
        {
            var entry = mapButtonToBackgroundMap.ElementAt(i);
            entry.Value.SetActive(i == index);
        }
    }

    public void Play()
    {
        if (selectedMapButton == Map1Button)
        {
            SceneManager.LoadScene(2);
        }
        else if (selectedMapButton == Map2Button)
        {
            SceneManager.LoadScene(3);
        }
        else if (selectedMapButton == Map3Button)
        {
            SceneManager.LoadScene(4);
        }
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.PlaySound(SoundManager.Sound.Click);
        }
    }
}
