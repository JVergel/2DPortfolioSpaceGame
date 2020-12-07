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
    public bool ShieldUp
    {
        get
        {
            return _shieldUp;
        }
    }
    public int powerUpVel
    {
        get
        {
            return _Powerupvel;
        }
    }

    public enum PowerUpById
    {
        tripleShot,
        SpeedUp,
        ShieldsUp
    }
    // Start is called before the first frame update
    private void Awake()
    {
        _tripleShot = false;
        _speedUp = false;
        _shieldUp = false;
        _Powerupvel     = 3;
        _ShieldVisual = GameObject.Find("Player2D").transform.Find("ShieldVisual").gameObject;

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
        float localwaitTime;
        switch (id)
        {
            
            case 0:
                _tripleShot = true;
                localwaitTime = 5f;
                StartCoroutine(powerUpBack(localwaitTime,id));
                break;
            case 1:
                _speedUp = true;
                localwaitTime = 10f;
                StartCoroutine(powerUpBack(localwaitTime,id));
                break;
            case 2:
                _shieldUp = true;
                _ShieldVisual.SetActive(true);

                break;
            default:
                break;
        }
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
