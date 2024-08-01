using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class AutoAddButtonHoverSound
{
    static AutoAddButtonHoverSound()
    {
        EditorApplication.hierarchyChanged += AddHoverSoundToAllButtons;
    }

    private static void AddHoverSoundToAllButtons()
    {
        Button[] buttons = Object.FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            if (button.gameObject.GetComponent<ButtonHoverSound>() == null)
            {
                button.gameObject.AddComponent<ButtonHoverSound>();
            }
        }

        Debug.Log("Hover sound added to all buttons in the scene.");
    }
}
