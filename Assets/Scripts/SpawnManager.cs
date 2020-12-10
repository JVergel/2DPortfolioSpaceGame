using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool stopSpawning = false;
    public bool StopSpawning
    {
        get
        {
            return stopSpawning;
        }
    }
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject[]powerUps;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void StartSpawning()
    {
        StartCoroutine(spwaningEnemies());
        StartCoroutine(powerUpSpawning());
    }
    // Update is called once per frame

    private IEnumerator spwaningEnemies()
    {
        while(!stopSpawning)
        {
            GameObject newEnemy = Instantiate(enemy,new Vector3(Random.Range(-8, 8), 9,0),Quaternion.identity);
            newEnemy.transform.parent = transform.gameObject.transform;
            yield return new WaitForSeconds(5f);
        }
        
    }
    private IEnumerator powerUpSpawning()
    {
        while (!stopSpawning)
        {
            GameObject powerUp = powerUps[Random.Range(0, powerUps.Length)];
            Instantiate(powerUp, new Vector3(Random.Range(-8, 8), 11, 0), Quaternion.identity);
          
            yield return new WaitForSeconds(Random.Range(15,20));
        }
    }
    public void OnPlayerDeath()
    {
        stopSpawning = true;

    }
}
