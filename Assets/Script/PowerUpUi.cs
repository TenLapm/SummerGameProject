using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PowerUpUi : MonoBehaviour
{
    public PowerUps Powerup;
    public int type;
    public float duration;
    public SpawnPointPowerUps count;
    public float scale;
    public int instant;
    public Sprite artwork;
    private float timer;
    private SpriteRenderer m_SpriteRenderer;
    [Header("LifeTime")]
    [SerializeField] private float maxTimer;


    void Start()
    {
        type = (int)Powerup.type;
        duration = (float)Powerup.duration;
        count = GetComponentInParent<SpawnPointPowerUps>();
        scale = (float)Powerup.scale;
        instant = (int)Powerup.instant;
        timer = maxTimer;
        m_SpriteRenderer = GetComponentInParent<SpriteRenderer>();
        artwork = Powerup.artwork;
        m_SpriteRenderer.sprite = artwork;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if( timer <= 0 )
        {
            Destroy(gameObject);
            count.count--;
        }
    }

    
}
