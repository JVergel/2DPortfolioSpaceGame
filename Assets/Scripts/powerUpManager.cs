using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpManager : MonoBehaviour
{
    private bool _tripleShot;
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private GameObject _ShieldVisual;
    [SerializeField]
    private Player _Player;
    [SerializeField]
    private AudioSource _PowerUpsound;


    public bool tripleShot
    {
        get
        {
            return _tripleShot;
        }
    }
    private bool _speedUp;
    public bool speedUp
    {
        get
        {
            return _speedUp;



        }
    }
    private int _Powerupvel = 0;
    private bool _shieldUp;
    private bool _ammoUp;
    private bool _lifeUp;
    private bool _crazyUp;
    private int _shieldForce = 0;
    public bool CrazyUp
    {
        get
        {
            return _crazyUp;
        }
    }
    public bool AmmoUp
    {
        get
        {
            return _ammoUp;
        }
    }
    public bool ShieldUp
    {
        get
        {
            return _shieldUp;
        }
    }
    public bool LifeUp
    {
        get
        {
            return _lifeUp;
        }
    }
    public int powerUpVel
    {
        get
        {
            return _Powerupvel;
        }
    }
    public int ShieldForce
    {
        get
        {
            return _shieldForce;
        }
    }
    public void SetShieldForce(int force)
    {
        if (force == 2)
        {
            _ShieldVisual.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (force == 1)
        {
            _ShieldVisual.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (force == 0)
        {
            _ShieldVisual.GetComponent<SpriteRenderer>().color = Color.white;
        }

        _shieldForce = force;
    }

    public enum PowerUpById
    {
        tripleShot,
        SpeedUp,
        ShieldsUp,
        AmmoUp,
        LifeUp,
        CrazyUp
    }
    // Start is called before the first frame update
    private void Awake()
    {

        _tripleShot = false;
        _speedUp = false;
        _shieldUp = false;
        _lifeUp = false;
        _crazyUp = false;
        
        _Powerupvel     = 3;
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _ShieldVisual = GameObject.Find("Player2D").transform.Find("ShieldVisual").gameObject;
        _PowerUpsound = GetComponent<AudioSource>();

    }
    public void powerUpMovement(Transform powerTransform,int velocity)
    {
        powerTransform.Translate(Vector3.down * Time.deltaTime * velocity);
        if(powerTransform.position.x>9 | powerTransform.position.x <-9 | powerTransform.position.y <-5.5f)
        {
            Destroy(powerTransform.gameObject);
        }
    }

    public void powerUpShotActivated(Transform powerTransform,int id)
    {
        ///id 0=> tripleshot
        ///id 1=> speed
        ///id 2=> shield
        ///id 3=> ammo
        ///id 4=> life
        ///id 5=> crazyshot
        float localwaitTime;
        switch (id)
        {

            case 0:
                _tripleShot = true;
                localwaitTime = 5f;
                StartCoroutine(powerUpBack(localwaitTime, id));
                break;
            case 1:
                _speedUp = true;
                localwaitTime = 10f;
                StartCoroutine(powerUpBack(localwaitTime, id));
                break;
            case 2:
                _shieldUp = true;
                SetShieldForce(3);
                _ShieldVisual.SetActive(true);
                break;
            case 3:
                _ammoUp = true;
                _Player.AddAmmo();
                break;
            case 4:
                _lifeUp = true;
                _Player.addHealth(1);
                break;
            case 5:
                _crazyUp = true;
                localwaitTime = 5f;
                StartCoroutine(powerUpBack(localwaitTime, id));
                break;


            default:
                break;
        }
        _PowerUpsound.Play();
        Destroy(powerTransform.gameObject);

    }
    public void powerUpShotDeactivated(int id)
    {
        switch (id)
        {
            case 0:
                _tripleShot = false;
                break;
            case 1:
                _speedUp = false;
                break;
            case 2:
                _shieldUp = false;
                _ShieldVisual.SetActive(false);
                break;
            case 3:
                _ammoUp = false;
                break;
            case 4:
                _lifeUp = false;
                break;
            case 5:
                _crazyUp = false;
                break;
            default:
                break;
        }
    }
    private IEnumerator powerUpBack(float wait,int id)
    {
        yield return new WaitForSeconds(wait);
        Debug.Log("done" + id);
        powerUpShotDeactivated(id);
        
    }

    // Update is called once per frame

}
