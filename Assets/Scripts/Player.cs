using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;


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
    [SerializeField]
    private int _ammo;
    [SerializeField]
    private GameObject[] WingDamage;
    private UIManager uIManager;
    private Animator _anim;
    [SerializeField]
    private AudioSource LazerSound;
    [SerializeField]
    private float MaxBoostTime;
    [SerializeField]
    private float ActualBoostTime;
    [SerializeField]
    private CShake shakeClass;
    private float NormalSpeed;
    private Coroutine chargeUp;
    private Coroutine chargeDown;
    [SerializeField]
    private int _MaxAmmo;
    public int Score
    {
        get 
        {
            return _score;
        }
    }
    public int Ammo
    {
        get
        {
            return _ammo;
        }
    }
    public int MaxAmmo
    {
        get
        {
            return _MaxAmmo;
        }
    }

    private bool tripleShot;

    [SerializeField]
    private GameObject tripleLazer;
    [SerializeField]
    private GameObject crazyLazer;
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
        _anim = this.GetComponent<Animator>();
        _ammo = 15;
        ActualBoostTime = MaxBoostTime;
        NormalSpeed = moveSpeed;

    }
    public float BoostPercentage()
    {
        return ActualBoostTime / MaxBoostTime; 
    }
    public void ChangeAnimation()
    {
        _anim.SetInteger("X_Axis", (int)vel.x);
        
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
        ChangeAnimation();
        transform.Translate(vel * Time.deltaTime * finalSpeed);
    }
    public void TurnOnDamage()
    {
        GameObject ThisWIng = WingDamage[Random.Range(0, 2)];
        if (!ThisWIng.activeSelf)
        {
            ThisWIng.SetActive(true);
        }
        else
        {
            TurnOnDamage();
        }
        
    }
    public void TurnOffDamage()
    {
        GameObject ThisWIng = WingDamage[Random.Range(0, 2)];
        if (ThisWIng.activeSelf)
        {
            ThisWIng.SetActive(false);
        }
        else
        {
            TurnOffDamage();
        }

    }
    public void addHealth(int Life)
    {
        if (health < 4)
        {
            health = health + Life;
            uIManager.changeLives(health);
            TurnOffDamage();
            powerUpManager.powerUpShotDeactivated((int)powerUpManager.PowerUpById.LifeUp);
        }

    }
    public void lowerHealth(int Damage)
    {
        shakeClass.Shake();
        if(powerUpManager.ShieldUp)
        {
            if (powerUpManager.ShieldForce == 3)
            {
                powerUpManager.SetShieldForce(2);
            }
            else if(powerUpManager.ShieldForce == 2)
            {
                powerUpManager.SetShieldForce(1);
            }
            else if (powerUpManager.ShieldForce == 1)
            {
                powerUpManager.SetShieldForce(0);
                powerUpManager.powerUpShotDeactivated((int)powerUpManager.PowerUpById.ShieldsUp);
            }
            
        }
        else
        {
            health = health - Damage;
            
            Debug.Log("lower");
            if (health <= 1)
            {
                spawnManager.OnPlayerDeath();
                uIManager.ChangeGameState(1);

                this.gameObject.SetActive(false);

                
                
            }
            else
            {
                TurnOnDamage();
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

            if (_ammo > 0)
            {
                _ammo -= 1;
                uIManager.changeAmmo();
                if (tripleShot)
                {
                    Instantiate(tripleLazer, new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), Quaternion.identity);
                }
                else if (powerUpManager.CrazyUp)
                {
                    Instantiate(crazyLazer, new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), Quaternion.identity);
                }
                else
                {
                    Instantiate(lazer, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                }
                LazerSound.Play();
            }
            
        }

        

    }
    public void Thrusters(InputAction.CallbackContext context)
    {

        if (context.performed == true)
        {

            if (ActualBoostTime> 0.05f)
            {

                if (chargeUp != null)
                {
                    StopCoroutine(chargeUp);
                }
                if (chargeDown != null)
                {
                    StopCoroutine(chargeDown);
                }

                chargeUp = StartCoroutine(TrustOn());
                
                
                

            }
            else
            {
                moveSpeed = NormalSpeed;
            }
            


        }
        if (context.canceled == true)
        {
            if (chargeUp != null)
            {
                StopCoroutine(chargeUp);
            }
            if(chargeDown!= null)
            {
                StopCoroutine(chargeDown);
            }
            
                chargeDown = StartCoroutine(TrustOff());
            
        }


    }
    IEnumerator TrustOn()
    {
        if (chargeDown != null)
        {
            StopCoroutine(chargeDown);
        }

        while (ActualBoostTime>0)
        {
            if (moveSpeed < NormalSpeed * 2)
            {
                moveSpeed = moveSpeed * 2;
            }
            
            ActualBoostTime -= 0.1f;
            uIManager.changeCharge();
            yield return new WaitForSeconds(0.1f);

        }
        chargeDown = StartCoroutine(TrustOff());


    }
    IEnumerator TrustOff()
    {
        if (chargeUp != null)
        {
            StopCoroutine(chargeUp);
        }

        moveSpeed = NormalSpeed;
        yield return new WaitForSeconds(1f);

        while (ActualBoostTime<MaxBoostTime)
        {
            
            ActualBoostTime += 0.1f;
            uIManager.changeCharge();
            yield return new WaitForSeconds(0.3f);

        }


    }
    public void AddScore(int Points)
    {
        _score += Points;
        uIManager.changeScore();
    }
    public void AddAmmo()
    {
        if (powerUpManager.AmmoUp && _ammo < _MaxAmmo)
        {
            _ammo+=(_MaxAmmo - Ammo);
            powerUpManager.powerUpShotDeactivated((int)powerUpManager.PowerUpById.AmmoUp);
        }
        uIManager.changeAmmo();
    }
    public void RestAmmo()
    {
        if (powerUpManager.AmmoUp && _ammo >0)
        {
            _ammo -= (Ammo/2);
            powerUpManager.powerUpShotDeactivated((int)powerUpManager.PowerUpById.AmmoUp);
        }
        uIManager.changeAmmo();
    }
}
