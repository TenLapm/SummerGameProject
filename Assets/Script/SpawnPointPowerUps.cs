using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointPowerUps : MonoBehaviour
{
    public int count = 0;
    public bool isMax = false;
    private float Timer = 0;
    private int num = 0;
    [SerializeField] private float axisX = 0;
    [SerializeField] private float axisY = 0;
    [SerializeField] private List<GameObject> PowerUp = new List<GameObject>();
    [SerializeField] private List<int> ratePowerUp = new List<int>();
    [SerializeField] private int limit = 5;
    [SerializeField] private float maxTimer = 5;
    [SerializeField] private float radius = 0.5f;


    void Start()
    {
        for (int i = 0; i < ratePowerUp.Count; i++) { 
            num += ratePowerUp[i];
        }
    }

    
    void Update()
    {
        if (count >= limit)
        {
            isMax = true;
        }
        if (!isMax)
        {
            SpawnPowerUp();
            Timer -= Time.deltaTime;
        }
        
    }

    void SpawnPowerUp()
    {
        if (count < limit && Timer <= 0)
        {
            
            int random = Random.Range(0, num);
            float randomX = Random.Range(-axisX / 2, axisX / 2);
            float randomY = Random.Range(-axisY / 2, axisY / 2);
            Vector2 pos = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
            Collider2D collider = Physics2D.OverlapCircle(pos, radius);

            if (collider == null)
            {
                int num2 = num;
                int type = ratePowerUp.Count - 1;
                for (int i = ratePowerUp.Count; i > 0; i--)
                {
                    if (random > num2 - ratePowerUp[i - 1])
                    {
                        GameObject newChild = Instantiate(PowerUp[type], pos, Quaternion.identity);
                        newChild.transform.parent = transform;
                        isMax = false;
                        count++;
                        Timer = maxTimer;
                        break;
                    }
                    num2 -= ratePowerUp[i - 1];
                    type--;
                }
                
            };
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector2(axisX, axisY));
    }
}
