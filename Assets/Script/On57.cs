using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On57 : MonoBehaviour
{
    private void OnEnable()
    {
        SoundManager.PlaySound(SoundManager.Sound.TimeEnd);
    }
}
