using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int enemyVelocity;
    [SerializeField]
    
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {

        if (transform.position.y < -7.2)
        {
            transform.position = new Vector3(Random.Range(-8.5f, 8.5f), -(transform.position.y-2f), transform.position.z);
        }
        else if (transform.position.x < -10)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 10)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        transform.Translate(Vector2.down * enemyVelocity * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.lowerHealth(1);
            }
            
            
            Destroy(transform.gameObject);
        }

        if (other.gameObject.CompareTag("Lazer"))
        {
            
            player.AddScore(10);
            Destroy(other.gameObject);
            Destroy(transform.gameObject);
        }

    }
}
