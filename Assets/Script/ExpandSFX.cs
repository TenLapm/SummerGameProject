using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandSFX : MonoBehaviour
{
    private void OnEnable()
    {
        SoundManager.PlaySound(SoundManager.Sound.Expand);
    }
}
