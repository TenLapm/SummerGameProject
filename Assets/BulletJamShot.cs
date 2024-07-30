using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletJamShot : MonoBehaviour
{
    private Rigidbody2D rb;
    private InventoryPowerUps inventoryPowerUp;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private Player player;


    [SerializeField] private GameObject trailSpawnPoint1;
    [SerializeField] private GameObject trailSpawnPoint2;
    [SerializeField] private GameObject trailSpawnPoint3;

    [SerializeField] private GameObject spritePrefab1;
    [SerializeField] private GameObject spritePrefab2;
    [SerializeField] private GameObject spritePrefab3;
    public float brushRadius;
    private GridManager gridManager;

    [SerializeField] private float spawnDistance = 1.0f;
    private Vector3 lastSpawnPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastSpawnPosition = transform.position;

        gridManager = FindObjectOfType<GridManager>();
    }


    void Update()
    {
        brushRadius = transform.localScale.x;
    }

    void FixedUpdate()
    {
        Moving();
        
        if (gridManager != null)
        {
            Vector3 playerPosition = transform.position;
            Vector2Int gridPosition = gridManager.WorldToGridPosition(transform.position);
            gridManager.ChangeTileOwner(gridPosition, player, brushRadius);
        }

        if (Vector3.Distance(transform.position, lastSpawnPosition) >= spawnDistance)
        {
            SpawnSprite();
            lastSpawnPosition = transform.position;
        }
    }
    private void Moving()
    {
        rb.velocity = transform.up * maxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            inventoryPowerUp.usingPowerUs = false;
            Destroy(gameObject);
        }
        if(collision.tag == "Player")
        {
            if (player != Player.PlayerA)
            {
                if (collision.gameObject.layer == 6)
                {
                    inventoryPowerUp.usingPowerUs = false;
                    Destroy(gameObject);
                }
                else if(collision.gameObject.layer == 7) 
                {
                    inventoryPowerUp = collision.GetComponent<InventoryPowerUps>();
                }
            }
            if (player != Player.PlayerB)
            {
                if (collision.gameObject.layer == 7)
                {
                    inventoryPowerUp.usingPowerUs = false;
                    Destroy(gameObject);
                }

                else if (collision.gameObject.layer == 6)
                {
                    inventoryPowerUp = collision.GetComponent<InventoryPowerUps>();
                }
            }
            
        }

    }

    public void SpawnSprite()
    {
        GameObject selectedPrefab = GetRandomSpritePrefab();
        Vector3 spawnPoint = GetRandomSpawnPoint();
        SpriteRenderer spriteRenderer = selectedPrefab.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = Mathf.RoundToInt(Time.time * 100);
        Instantiate(selectedPrefab, spawnPoint, Quaternion.identity);
    }

    private GameObject GetRandomSpritePrefab()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                return spritePrefab1;
            case 1:
                return spritePrefab2;
            case 2:
                return spritePrefab3;
            default:
                return spritePrefab1;
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                return trailSpawnPoint1.transform.position;
            case 1:
                return trailSpawnPoint2.transform.position;
            case 2:
                return trailSpawnPoint3.transform.position;
            default:
                return trailSpawnPoint1.transform.position;
        }
    }
    private Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        float gridWidth = gridManager.cols * gridManager.tileSize;
        float gridHeight = gridManager.rows * gridManager.tileSize;
        Vector3 startPos = new Vector3(-gridWidth / 2 + gridManager.tileSize / 2, -gridHeight / 2 + gridManager.tileSize / 2, 0);

        Vector3 localPos = worldPosition - startPos;
        int col = Mathf.FloorToInt(localPos.x / gridManager.tileSize);
        int row = Mathf.FloorToInt(localPos.y / gridManager.tileSize);

        return new Vector2Int(col, row);
    }
}
