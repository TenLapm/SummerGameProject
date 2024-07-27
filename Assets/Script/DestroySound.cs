using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySound : MonoBehaviour
{
    GameManager gameManager;
    public float delay = 180;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>(); 
    }
    private void Start()
    {
        delay = 180;
        Destroy(gameObject, delay);
    }
}
