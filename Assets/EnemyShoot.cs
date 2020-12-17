using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject Lazer;
    [SerializeField]
    private bool SmartOn;
    private bool CanShoot;
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
        if (!SmartOn)
        {
            StartCoroutine(Shot());
        }
        _player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void Update()
    {
        if (SmartOn&& (this.transform.position.y<_player.transform.position.y)&& CanShoot)
        {
            Instantiate(Lazer, this.transform.position, Quaternion.identity);
            StartCoroutine(CanShootSmart());
        }
    }

    // Update is called once per frame
    private IEnumerator CanShootSmart()
    {
        CanShoot = false;
        yield return new WaitForSeconds(0.5f);
        CanShoot = true;
    }
    private IEnumerator Shot()
    {
        while (true)
        {
            int wait = Random.Range(2, 10);
            yield return new WaitForSeconds(wait);
            Instantiate(Lazer,this.transform.position,Quaternion.identity);
        }

        
    }
}
