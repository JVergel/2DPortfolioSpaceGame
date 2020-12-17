using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject Lazer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shot());
    }

    // Update is called once per frame
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
