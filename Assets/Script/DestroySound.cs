using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySound : MonoBehaviour
{
    public float delay = 180;

    private void Start()
    {
        delay = 180;
        Destroy(gameObject, delay);
    }
}
