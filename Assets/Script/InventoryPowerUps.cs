using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using static UnityEditor.PlayerSettings;
//using static UnityEditor.PlayerSettings;

public class InventoryPowerUps : MonoBehaviour
{
    public bool HavePowerUp = false;
    public bool usingPowerUs = false;
    [SerializeField] private CircleCollider2D Explosion;
    public Animator animator;
    [SerializeField] private GameObject EfxObject;
    [SerializeField] private Rigidbody2D efxRigi;
    [SerializeField] private SpriteRenderer efxSprite;
    [SerializeField] private BoxCollider2D checkJamShot;
    public int type;
    public PowerUps PowerUp;
    private float duration;
    private Vector3 scale;
    private SpawnPointPowerUps spawnPointPowerUps;
    private List<GameObject> Clone = new List<GameObject>();
    private GameObject Gclone;
    private GameObject newJam;
    [SerializeField] private GameObject Jam;
    [SerializeField] private GameObject BulletJam;
    private int instant;
    private PlayerControl playerControl;
    private bool once = true;
    private float animTime = 0.3f;
    private Vector3 nowPos;
    private Player player;

    [Header("Color Effect Clone")]
    [SerializeField] int R = 255;
    [SerializeField] int G = 255;
    [SerializeField] int B = 255;

    private GridManager gridManager;
    private InGameUI gameUI;

    [SerializeField] private GameObject expandSFX;
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        gridManager = FindObjectOfType<GridManager>();
        gameUI = FindObjectOfType<InGameUI>();
    }

    void Update()
    {
        if (gameUI.countdownActive == false)
        {
            UsingPowerUps();
            PowerUpsDuration();
            anim();
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PowerUp" && !HavePowerUp && !usingPowerUs)
        {
            Debug.Log("Pickup");
            SoundManager.PlaySound(SoundManager.Sound.PickUp);
            HavePowerUp = true;
            PowerUp = other.GetComponent<PowerUpUi>().Powerup;
            spawnPointPowerUps = other.GetComponent<PowerUpUi>().count;
            type = (int)PowerUp.type;
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
        if(instant == 1)
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
                    expandSFX.SetActive(true);
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
                    if (usingPowerUs == false)
                    {
                        efxSprite.color = Color.white;
                        efxRigi.constraints = RigidbodyConstraints2D.FreezeAll;
                        EfxObject.transform.SetParent(null);
                        nowPos = transform.position;
                        EfxObject.transform.position = nowPos;
                        animator.SetBool("IsJamExplosion", true);
                        once = false;
                        usingPowerUs = true;
                        animTime = 0.25f;
                    }
                    
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
                    efxSprite.color = Color.white;
                    efxRigi.simulated = false;
                    efxRigi.constraints = RigidbodyConstraints2D.None;
                    EfxObject.transform.SetParent(transform, true);
                    EfxObject.transform.position = transform.position;
                    EfxObject.transform.rotation = EfxObject.transform.parent.rotation;
                    duration = PowerUp.duration;
                    Explosion.enabled = true;
                    usingPowerUs = true;
                    Explosion.radius = PowerUp.scale;
                    animator.SetTrigger("IsExplosion");
                    SoundManager.PlaySound(SoundManager.Sound.Blast2);
                    break;
                case 3:
                    efxSprite.color = new Color(R, G, B);
                    animTime = 0.25f;
                    efxRigi.simulated = false;
                    efxRigi.constraints = RigidbodyConstraints2D.None;
                    EfxObject.transform.SetParent(transform, true);
                    EfxObject.transform.position = transform.position;
                    EfxObject.transform.rotation = transform.rotation;
                    animator.SetTrigger("IsExplosion");
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
                    //Vector3 playerPos = transform.position;
                    //Vector3 playerDirection = transform.up;
                    //float spawnDistance = 1f;

                    //for (float i = 0; i < PowerUp.scale; i++)
                    //{
                    //    Vector3 spawnPos = playerPos + playerDirection * (spawnDistance + (i/2));
                    //    Collider2D collider = Physics2D.OverlapCircle(spawnPos, 0.1f);
                    //    if (collider != null)
                    //    {
                    //        break;
                    //    }
                    //    GameObject newJam = Instantiate(Jam, spawnPos, Quaternion.identity);
                    //}
                    duration = PowerUp.duration;
                    newJam = Instantiate(BulletJam, transform.position, transform.rotation);
                    newJam.transform.localScale = new Vector3(PowerUp.scale, PowerUp.scale, PowerUp.scale);
                    usingPowerUs = true;
                    SoundManager.PlaySound(SoundManager.Sound.Puke);
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
                    efxSprite.color = Color.white;
                    efxRigi.simulated = false;
                    efxRigi.constraints = RigidbodyConstraints2D.None;
                    EfxObject.transform.SetParent(transform, true);
                    EfxObject.transform.position = transform.position;
                    EfxObject.transform.rotation = transform.rotation;
                    duration = PowerUp.duration;
                    Explosion.enabled = true;
                    usingPowerUs = true;
                    Explosion.radius = PowerUp.scale;
                    animator.SetTrigger("IsExplosion");
                    SoundManager.PlaySound(SoundManager.Sound.Blast2);
                    break;
                case 3:
                    efxSprite.color = new Color(R, G, B);
                    animTime = 0.25f;
                    efxRigi.simulated = false;
                    efxRigi.constraints = RigidbodyConstraints2D.None;
                    EfxObject.transform.SetParent(transform, true);
                    EfxObject.transform.position = transform.position;
                    EfxObject.transform.rotation = transform.rotation;
                    animator.SetTrigger("IsExplosion");
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
                    //Vector3 playerPos = transform.position;
                    //Vector3 playerDirection = transform.up;
                    //float spawnDistance = 1f;

                    //for (float i = 0; i < PowerUp.scale; i++)
                    //{
                    //    Vector3 spawnPos = playerPos + playerDirection * (spawnDistance + (i/2));
                    //    Collider2D collider = Physics2D.OverlapCircle(spawnPos, 0.1f);
                    //    if (collider != null)
                    //    {
                    //        break;
                    //    }
                    //    GameObject newJam = Instantiate(Jam, spawnPos, Quaternion.identity);
                    //}
                    duration = PowerUp.duration;
                    newJam = Instantiate(BulletJam, transform.position, transform.rotation);
                    newJam.transform.localScale = new Vector3(PowerUp.scale, PowerUp.scale, PowerUp.scale);
                    usingPowerUs = true;
                    SoundManager.PlaySound(SoundManager.Sound.Puke);
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
                    instant = 0;
                    transform.localScale = scale;
                    usingPowerUs = false;
                    expandSFX.SetActive(false);
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
                    SoundManager.PlaySound(SoundManager.Sound.Cloneoff);
                    Clone.Clear();
                    break;

                case 4:
                    instant = 0;
                    usingPowerUs = false;
                    type = 0;
                    break;

                case 5:
                    usingPowerUs = false;
                    Destroy(newJam);
                    break;
            }
        }
    }

    private void anim()
    {
        if (animTime > 0)
        {
            animTime -= Time.deltaTime;
        }

        if(animTime <= 0)
        {
            animator.SetBool("IsJamExplosion", false);
            if (!once)
            {
                GameObject newJam = Instantiate(Jam, nowPos, Quaternion.Euler(new Vector3(0, 0, 0)));
                newJam.transform.localScale = new Vector3(PowerUp.scale, PowerUp.scale, PowerUp.scale);
                once = true;
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
    IEnumerator wait(int num)
    {
        yield return new WaitForSeconds(num);
    }
}
