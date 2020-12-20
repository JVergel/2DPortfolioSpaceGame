using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private UIManager Umanager;
    private bool stopSpawning = false;
    public bool StopSpawning
    {
        get
        {
            return stopSpawning;
        }
    }
    [SerializeField]
    private GameObject[] enemy;
    [SerializeField]
    [Header("----Enemies Rarity----")]
    private int[] weigthChancesEnemies;
    [SerializeField]

    private GameObject[] powerUps;

    [SerializeField]
    [Header("----PowerUp Rarity----")]
    private int[] weigthChancesPowerUps;
    private int _currentWave;
    [SerializeField]
    private int NumberOfEnemies;
    private int RateSpawn;
    [SerializeField]
    public int EnemiesAlive;
    [SerializeField]
    private bool BossWave;
    [SerializeField]
    private GameObject boss;
    private bool bossDefeated;

    // Start is called before the first frame update
    void Start()
    {
        _currentWave = 0;
        NumberOfEnemies = 0;
        RateSpawn = 0;
        BossWave = false;
        bossDefeated=false;
    }
    public void StartSpawning()
    {
        StartCoroutine(spwaningEnemies());
        StartCoroutine(powerUpSpawning());
    }
    // Update is called once per frame

    private IEnumerator spwaningEnemies()
    {

        CuerrentWaveEnemies();
        while (!stopSpawning)
        {
            if (BossWave)
            {
                GameObject bosse = Instantiate(boss, new Vector3(Random.Range(-8, 8), 9, 0), Quaternion.identity);
                bosse.transform.parent = transform.gameObject.transform;
                yield return new WaitForSeconds(10);
                bossDefeated = true;
                BossWave = false;
            }
            yield return new WaitForSeconds(1f);

                StartCoroutine(Umanager.FlickNewWave(_currentWave));
                GameObject Enemy = enemy[GetRandomWeightedIndex(weigthChancesEnemies)];
                GameObject newEnemy = Instantiate(Enemy, new Vector3(Random.Range(-8, 8), 9, 0), Quaternion.identity);
                newEnemy.transform.parent = transform.gameObject.transform;
                NumberOfEnemies--;



            if (bossDefeated)
            {
                _currentWave = 100;
                CuerrentWaveEnemies();

            }
            else
            {
                yield return new WaitForSeconds(RateSpawn);
                for (int i = 0; i < NumberOfEnemies || EnemiesAlive > 0; NumberOfEnemies--)
                {
                    if (NumberOfEnemies < 0)
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        Enemy = enemy[GetRandomWeightedIndex(weigthChancesEnemies)];
                        newEnemy = Instantiate(Enemy, new Vector3(Random.Range(-8, 8), 9, 0), Quaternion.identity);
                        newEnemy.transform.parent = transform.gameObject.transform;
                        yield return new WaitForSeconds(RateSpawn);

                    }

                }
                

            }
            Debug.Log("this wave"+_currentWave);
            

            yield return new WaitForSeconds(4f);
            _currentWave++;
            CuerrentWaveEnemies();




        }
        
    }
    private void CuerrentWaveEnemies()
    {
        switch (_currentWave)
        {
            case 0:
                RateSpawn = 5;
                NumberOfEnemies = 4;
                break;
            case 1:
                RateSpawn = 5;
                NumberOfEnemies = 10;
                break;
            case 2:
                RateSpawn = 4;
                NumberOfEnemies = 10;
                break;
            case 3:
                RateSpawn = 4;
                NumberOfEnemies = 15;
                break;
            case 4:
                BossWave = true;
                
                break;
            default:
                stopSpawning = true;
                Debug.Log("All waves Done");
                break;







        }
    }
    private IEnumerator powerUpSpawning()
    {
        while (!stopSpawning)
        {
            GameObject powerUp = powerUps[GetRandomWeightedIndex(weigthChancesPowerUps)];
            Instantiate(powerUp, new Vector3(Random.Range(-8, 8), 11, 0), Quaternion.identity);
          
            yield return new WaitForSeconds(Random.Range(5,12));
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


