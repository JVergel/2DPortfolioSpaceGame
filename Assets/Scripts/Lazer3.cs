using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer3 : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    private float BulletSpeed;
    [SerializeField]
    private AudioSource ExplodeSound;
    [SerializeField]
    public AudioClip clip;

    // Start is called before the first frame update
    private void Awake()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime *  BulletSpeed,Space.Self);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _player.GetComponent<Player>().lowerHealth(1);
            Destroy(this.GetComponent<Collider2D>());
            Destroy(this.GetComponent<SpriteRenderer>());
            BulletSpeed = 0;
            ExplodeSound.Play();
            Destroy(this.gameObject, clip.length);
        }
        
    }
}
