using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float setTime = 60;
    public float CurrentTime => setTime;

    private InGameUI gameUI;

    private void Awake()
    {
        gameUI = FindObjectOfType<InGameUI>();
    }
    private void Update()
    {
        if(gameUI.countdownActive == false)
        {
            if (Time.timeScale == 1.0f)
            {
                setTime -= Time.deltaTime;
            }
        }
        
    }

    public void ResetTimer()
    {
        setTime = 60f;
    }
}
