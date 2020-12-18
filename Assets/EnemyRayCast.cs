using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRayCast : MonoBehaviour
{
    [SerializeField]
    private GameObject Lazer;
    private bool Wait;
    // Start is called before the first frame update
    void Start()
    {
        Wait = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("PowerUp") && Wait == false)
            {
                Debug.Log("RayCastWorking");
                Instantiate(Lazer, this.transform.position, Quaternion.identity);
                StartCoroutine(Shot());

            }
        }

    }
    private IEnumerator Shot()
    {
        Wait = true;
        yield return new WaitForSeconds(1f);
        Wait = false;
    }
}
