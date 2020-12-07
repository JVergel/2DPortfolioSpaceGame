using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField]
    private Vector2 vel;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    public float bulletSpeed;
    [SerializeField]
    private GameObject lazer;
    [SerializeField]
    private int health;
    [SerializeField]
    private SpawnManager spawnManager;
    [SerializeField]
    private powerUpManager powerUpManager;
    [SerializeField]
    private int _score;
    private UIManager uIManager;
    public int Score
    {
        get 
        {
            return _score;
        }
    }

    private bool tripleShot;

    [SerializeField]
    private GameObject tripleLazer;
    private float finalSpeed;

    private void Awake()
    {
        
        spawnManager = GameObject.Find("LevelManager").GetComponent<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("null spawn");
        }

        powerUpManager = GameObject.Find("LevelManager").GetComponent<powerUpManager>();
        if (powerUpManager == null)
        {
            Debug.LogError("null spawn");
        }
        uIManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();

    }

    public void Update()
    {
        
        PlayerMovement();
    }
    private void PlayerMovement()
    {
        if (transform.position.y > 8)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }
        else if (transform.position.y < -8)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -8.9)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > 8.9)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
        if (powerUpManager.speedUp)
        {
            finalSpeed = moveSpeed * 2;
        }
        else
        {
            finalSpeed = moveSpeed;
        }
        transform.Translate(vel * Time.deltaTime * finalSpeed);
    }
    public void lowerHealth(int Damage)
    {
        if(powerUpManager.ShieldUp)
        {
            powerUpManager.powerUpShotDeactivated((int) powerUpManager.PowerUpById.ShieldsUp);
        }else
        {
            health = health - Damage;
            
            Debug.Log("lower");
            if (health <= 0)
            {
                spawnManager.OnPlayerDeath();
                uIManager.ChangeGameState(1);

                this.gameObject.SetActive(false);

                
                
            }
            uIManager.changeLives(health);
        }

;
    }
    public void Move(InputAction.CallbackContext context)
    {
        vel = context.ReadValue<Vector2>(); 
    }
    public void Fire(InputAction.CallbackContext context)
    {
        tripleShot = powerUpManager.tripleShot;
        if (context.started == true)
        {
            if (tripleShot)
            {
                Instantiate(tripleLazer, new Vector3(transform.position.x, transform.position.y-1.5f, transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(lazer, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            }
        }

    }

    public void AddScore(int Points)
    {
        _score += Points;
        uIManager.changeScore();
    }
}
