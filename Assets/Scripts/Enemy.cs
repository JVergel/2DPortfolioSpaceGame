using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

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
    [SerializeField]
    private bool ShieldOn;
    [SerializeField]
    private GameObject Shield;
    [SerializeField]
    private int EnemyMovementId;
    private Vector2 target;
    private float step;
    private AvoidArea avoidArea;

    // Start is called before the first frame update
    void Start()
    {
  
            avoidArea = GetComponentInChildren<AvoidArea>();
        

        
        Manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<SpawnManager>();
        Manager.EnemiesAlive++;
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        catch (Exception e)
        {
            Debug.Log("Player is death or missing from scene");
        }
        if (player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        ExplodeSound = GetComponent<AudioSource>();
        if (ShieldOn)
        {
            Shield.SetActive(true);
        }
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
        if (EnemyMovementId == 0)
        {
            transform.Translate(new Vector2(Mathf.Sin(-1 * enemyVelocity * Time.time) * 0.02f, -1 * enemyVelocity * Time.deltaTime));
        }
        else if (EnemyMovementId == 1)
        {
            if (Vector2.Distance(this.gameObject.transform.position, player.transform.position)<5)
            {
                target = player.transform.position;
                var dir = player.transform.position - transform.position;
                var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) +90;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                step = (enemyVelocity/1.5f) * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target, step);
                this.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                transform.Translate(new Vector2(Mathf.Sin(-1 * enemyVelocity * Time.time) * 0.02f, -1 * enemyVelocity * Time.deltaTime),Space.World);
                transform.rotation = Quaternion.identity;
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else if (EnemyMovementId == 2)
        {
            transform.Translate(new Vector2(Mathf.Sin(-1 * enemyVelocity * Time.time) * 0.02f, -1 * enemyVelocity * Time.deltaTime), Space.World);
            if (avoidArea.Avoid)
            {
                transform.Translate(new Vector2(-1 * enemyVelocity * Time.deltaTime * 2f, -1 * enemyVelocity * Time.deltaTime*0.2f), Space.World);
            }
            
        }
        else if (EnemyMovementId == 3)
        {

        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ShieldOn)
        {
            ShieldOn = false;
            Shield.SetActive(false);
        }
        else
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

                Destroy(transform.gameObject, 2.8f);
            }

            if (other.gameObject.CompareTag("Lazer"))
            {

                player.AddScore(10);
                Destroy(other.gameObject);
                coll.enabled = false;
                ExplodeSound.Play();
                anim.SetTrigger("EnemyIsDeath");
                Destroy(transform.gameObject, 2.8f);
            }
        }
    }
    private void OnDestroy()
    {
        Manager.EnemiesAlive--;
    }
}
