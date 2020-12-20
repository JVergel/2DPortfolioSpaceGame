using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer4 : MonoBehaviour
{
    private GameObject[] Enemies;
    private GameObject target;
    private GameObject player;
    private Vector3 TargetPos;
    private float step;
    [SerializeField]
    private float BulletSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        Destroy(this.gameObject, 5f);
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        try
        {
            target = Enemies[0];
        }
        catch
        {
            Destroy(this.gameObject);
            Debug.LogError("No Target");
        }
    }
    void Start()
    {

        
        for (int i = 0; i < Enemies.Length; i++)
        {
            if(Vector2.Distance(target.transform.position,player.transform.position)> Vector2.Distance(Enemies[i].transform.position, player.transform.position))
            {
                target = Enemies[i];
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y < -7.2)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x < -10)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.x > 10)
        {
            Destroy(this.gameObject);
        }
        else
        {
            TargetPos = target.transform.position;
            var dir = TargetPos - transform.position;
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            step = BulletSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, TargetPos, step);
        }



    }
}
