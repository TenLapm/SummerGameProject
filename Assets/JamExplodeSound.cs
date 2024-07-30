using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamExplodeSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.Blast);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
