using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int enemyVelocity;
    [SerializeField]
    
    private Player player;
    private Animator anim;
    private Collider2D coll;
    [SerializeField]
    private AudioSource ExplodeSound;
    private SpawnManager Manager;

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<SpawnManager>();
        Manager.EnemiesAlive++;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        ExplodeSound = GetComponent<AudioSource>();
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
        transform.Translate(new Vector2(Mathf.Sin(-1 * enemyVelocity *Time.time) * 0.02f, -1* enemyVelocity * Time.deltaTime ));
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
            coll.enabled = false;
            anim.SetTrigger("EnemyIsDeath");
            ExplodeSound.Play();
           
            Destroy(transform.gameObject,2.8f);
        }

        if (other.gameObject.CompareTag("Lazer"))
        {
            
            player.AddScore(10);
            Destroy(other.gameObject);
            coll.enabled = false;
            ExplodeSound.Play();
            anim.SetTrigger("EnemyIsDeath");
            Destroy(transform.gameObject,2.8f);
        }

    }
    private void OnDestroy()
    {
        Manager.EnemiesAlive--;
    }
}
