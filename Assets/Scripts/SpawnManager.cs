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
    [SerializeField]
    private int[] weigthChances;
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
            GameObject powerUp = powerUps[GetRandomWeightedIndex(weigthChances)];
            Instantiate(powerUp, new Vector3(Random.Range(-8, 8), 11, 0), Quaternion.identity);
          
            yield return new WaitForSeconds(Random.Range(15,20));
        }
    }
    public void OnPlayerDeath()
    {
        stopSpawning = true;

    }
    public int GetRandomWeightedIndex(int[] weights)
    {
        int totalWeigth = System.Linq.Enumerable.Sum(weights);
        int randomWeigth = Random.Range(0, totalWeigth);
        int currentWeigth = 0;

        for(int i=0;i<weights.Length; i++)
        {
            currentWeigth += weights[i];
            if (randomWeigth <= currentWeigth)
            {
                return i;
                
            }
        }
        return -1;
    }
}


