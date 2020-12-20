using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private int BossSpeed;
    [SerializeField]
    private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shot());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), BossSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, 0));
        transform.Rotate(0, 0, 10);
        if (transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }



    }
    private IEnumerator Shot()
    {
        int i = 100;
        while (i>0)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(bullet, transform.position, transform.rotation);
            i--;
        }

    }
    

}
