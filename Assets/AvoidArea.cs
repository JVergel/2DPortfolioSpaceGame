using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidArea : MonoBehaviour
{
    private bool _Avoid;
    public bool Avoid
    {
        get
        {
            return _Avoid;
        }
    }
    private void Start()
    {
        _Avoid = false;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lazer"))
        {
            _Avoid = true;
        }
            
        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_Avoid)
        {
            StartCoroutine(AvoidToFalse());
        }
        
    }
    IEnumerator AvoidToFalse()
    {
        yield return new WaitForSeconds(0.25f);
        _Avoid = false;
    }
}
