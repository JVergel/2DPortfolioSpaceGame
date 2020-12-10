using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float RotateSpeed=3.0f;
    [SerializeField]
    private GameObject Explote;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * RotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lazer"))
        {
            Instantiate(Explote, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);

            
            Destroy(this.gameObject,0.2f);
        }
    }
}
