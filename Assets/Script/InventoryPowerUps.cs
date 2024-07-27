using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
//using static UnityEditor.PlayerSettings;

public class InventoryPowerUps : MonoBehaviour
{
    public bool HavePowerUp = false;
    public bool usingPowerUs = false;
    [SerializeField] private CircleCollider2D Explosion;
    private int type;
    private PowerUps PowerUp;
    private float duration;
    private Vector3 scale;
    private SpawnPointPowerUps spawnPointPowerUps;
    private List<GameObject> Clone = new List<GameObject>();
    private GameObject Gclone;
    [SerializeField] private GameObject Jam;
    private int instant;
    private PlayerControl playerControl;
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    void Update()
    {
        UsingPowerUps();
        PowerUpsDuration();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUp" && !HavePowerUp && !usingPowerUs)
        {
            Debug.Log("Pickup");
            HavePowerUp = true;
            PowerUp = other.GetComponent<PowerUpUi>().Powerup;
            spawnPointPowerUps = other.GetComponent<PowerUpUi>().count;
            if (PowerUp.type == powerUpType.Clone) 
            {
                if (playerControl.player == Player.PlayerA)
                {
                    Gclone = PowerUp.Clone[0];
                }
                else if (playerControl.player == Player.PlayerB)
                {
                    Gclone = PowerUp.Clone[1];
                } 
            }
            spawnPointPowerUps.count--;
            instant = other.GetComponent<PowerUpUi>().instant;
            Destroy(other.gameObject);
        }
        
    }

    public void UsingPowerUps()
    {
        if(instant == 1 && HavePowerUp && !usingPowerUs)
        {
            type = (int)PowerUp.type;
            HavePowerUp = false;
            switch (type)
            {
                case 0:
                    break;
                case 1:
                    duration = PowerUp.duration;
                    scale = transform.localScale;
                    transform.localScale = new Vector3(PowerUp.scale, PowerUp.scale, PowerUp.scale);
                    usingPowerUs = true;
                    break;
                case 2:
                    usingPowerUs = true;
                    break;
                case 3:

                    for (int i = 0; i < PowerUp.scale; i++)
                    {
                        float axisz = (360.0f / (PowerUp.scale + 1));
                        GameObject c = Instantiate(Gclone, transform.position, Quaternion.Euler(new Vector3(0, 0, (axisz * (i + 1)) + transform.rotation.eulerAngles.z)));
                        duration = PowerUp.duration;
                        Clone.Add(c);
                    }
                    usingPowerUs = true;
                    break;
                    
                case 4:
                    GameObject newJam = Instantiate(Jam, transform.position, Quaternion.identity);
                    newJam.transform.localScale = new Vector3(PowerUp.scale, PowerUp.scale, PowerUp.scale);
                    break;
            }
        }
        if(Input.GetKeyDown(KeyCode.E) && HavePowerUp && !usingPowerUs && gameObject.layer == 6)
        {
            type = (int)PowerUp.type;
            HavePowerUp = false;
            switch(type)
            {
                case 0:
                    break;
                case 1:
                    duration = PowerUp.duration;
                    scale = transform.localScale;
                    transform.localScale = new Vector3(3f, 3f, 3f);
                    usingPowerUs = true;
                    break;
                case 2:
                    duration = PowerUp.duration;
                    Explosion.enabled = true;
                    usingPowerUs = true;
                    Explosion.radius = PowerUp.scale;
                    break;
                case 3:
                    
                    for(int i = 0; i < PowerUp.scale; i++)
                    {
                        float axisz = (360.0f / (PowerUp.scale + 1));
                        GameObject c = Instantiate(Gclone, transform.position, Quaternion.Euler(new Vector3(0, 0, (axisz * (i + 1)) + transform.rotation.eulerAngles.z)));
                        duration = PowerUp.duration;
                        Clone.Add(c); 
                    }
                    usingPowerUs = true;
                    break;
                case 5:
                    Vector3 playerPos = transform.position;
                    Vector3 playerDirection = transform.up;
                    float spawnDistance = 1f;
                    
                    for (float i = 0; i < PowerUp.scale; i++)
                    {
                        Vector3 spawnPos = playerPos + playerDirection * (spawnDistance + (i/2));
                        GameObject newJam = Instantiate(Jam, spawnPos, Quaternion.identity);
                    }
                    usingPowerUs = true;
                    break;
            }
        }
        if(Input.GetKeyDown(KeyCode.Keypad0) && HavePowerUp && !usingPowerUs && gameObject.layer == 7)
        {
            type = (int)PowerUp.type;
            HavePowerUp = false;
            switch (type)
            {
                case 0:
                    break;
                case 1:
                    duration = PowerUp.duration;
                    scale = transform.localScale;
                    transform.localScale = new Vector3(3f, 3f, 3f);
                    usingPowerUs = true;
                    break;
                case 2:
                    duration = PowerUp.duration;
                    Explosion.enabled = true;
                    usingPowerUs = true;
                    Explosion.radius = PowerUp.scale;
                    
                    break;
                case 3:

                    for (int i = 0; i < PowerUp.scale; i++)
                    {
                        float axisz = (360.0f / (PowerUp.scale + 1));
                        GameObject c = Instantiate(Gclone, transform.position, Quaternion.Euler(new Vector3(0, 0, (axisz * (i + 1)) + transform.rotation.eulerAngles.z)));
                        duration = PowerUp.duration;
                        Clone.Add(c);
                    }
                    usingPowerUs = true;
                    break;
                case 5:
                    Vector3 playerPos = transform.position;
                    Vector3 playerDirection = transform.up;
                    float spawnDistance = 1f;

                    for (float i = 0; i < PowerUp.scale; i++)
                    {
                        Vector3 spawnPos = playerPos + playerDirection * (spawnDistance + (i / 2));
                        GameObject newJam = Instantiate(Jam, spawnPos, Quaternion.identity);
                    }
                    usingPowerUs = true;
                    break;
            }
        }
    }

    public void PowerUpsDuration()
    {

        if (usingPowerUs)
        {
            duration -= Time.deltaTime;
        }
        if (duration <= 0 && usingPowerUs)
        {
            switch (type)
            {
                case 0:
                    break;
                case 1:
                    transform.localScale = scale;
                    usingPowerUs = false;
                    break;
                case 2:
                    Explosion.enabled = false;
                    usingPowerUs = false;
                    break;
                case 3:
                    usingPowerUs = false;
                    for (int i = 0; i < PowerUp.scale; i++)
                    {
                        Destroy(Clone[i]);
                    }
                    Clone.Clear();
                    break;

                case 4:
                    break;

                case 5:
                    usingPowerUs = false;
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 playerPos = transform.position;
        Vector2 playerDirection = transform.up;
        float spawnDistance = 1f;
        Vector2 spawnPos = playerPos + playerDirection * (spawnDistance * 3);
        Gizmos.DrawLine(transform.position, spawnPos);
    }
}
