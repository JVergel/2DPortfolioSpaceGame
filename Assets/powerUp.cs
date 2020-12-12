using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{

    [SerializeField]
    private powerUpManager powerUpManager;
    [SerializeField]
    private int powerupID;

    // Start is called before the first frame update
    void Start()
    {
        powerUpManager = GameObject.Find("LevelManager").GetComponent<powerUpManager>();

    }

    // Update is called once per frame
    void Update()
    {
        powerUpManager.powerUpMovement(transform, powerUpManager.powerUpVel);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

                powerUpManager.powerUpShotActivated(this.gameObject.transform,powerupID);
            

            
        }
    }
}
