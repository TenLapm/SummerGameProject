using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditBGMStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.CreditBGM);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
