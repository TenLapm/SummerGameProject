using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIcon : MonoBehaviour
{
    [SerializeField] private InventoryPowerUps inventoryPowerUps;

    [SerializeField] private Sprite artwork1;
    [SerializeField] private Sprite artwork2;
    [SerializeField] private Sprite artwork3;
    [SerializeField] private Sprite artwork4;
    [SerializeField] private Sprite artwork5;

    [SerializeField] private SpriteRenderer image;
    void Start()
    {
        image.sprite = null;
    }

    void Update()
    {
        if (inventoryPowerUps.HavePowerUp) {
            switch (inventoryPowerUps.type)
            {

                case 0:
                    image.sprite = null;
                    
                    break;
                case 1:
                    image.sprite = artwork1;
                    break;
                case 2:
                    image.sprite = artwork2;
                    break;
                case 3:
                    image.sprite = artwork3;
                    break;
                case 4:
                    image.sprite = artwork4;
                    break;
                case 5:
                    image.sprite = artwork5;
                    break;
            }
        }
        else
        {
            image.sprite = null;
        }
    }
}
