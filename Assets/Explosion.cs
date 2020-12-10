using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<SpawnManager>();
        Invoke("Spawn", 1f);
        Destroy(this.gameObject, 3f);
    }
    private void Spawn()
    {
        Debug.Log("startSpawn");
        _spawnManager.StartSpawning();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
