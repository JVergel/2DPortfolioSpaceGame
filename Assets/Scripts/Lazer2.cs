using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer2 : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    private float BulletSpeed;
    [SerializeField]
    private AudioSource ExplodeSound;
    [SerializeField]
    public AudioClip clip;
    private Vector2 target;
    private float step;

    // Start is called before the first frame update
    private void Awake()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player");
        Destroy(this.gameObject, 5f);
    }
    void Start()
    {
        target = _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        target = _player.transform.position;
        var dir = _player.transform.position - transform.position;
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg)-90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        step = BulletSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
        
        if (transform.position.y<-6f)
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
