using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime *  player.bulletSpeed);
        if (transform.position.y>6f)
        {
            if (transform.parent == null)
            {
                Destroy(transform.gameObject);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }

        }
    }
}
