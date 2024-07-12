using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolMember : MonoBehaviour
{
    public ArrowPool pool;

    private void OnBecameInvisible(){
        pool.Kill(this);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") )
        {
            pool.Kill(this);
        }
    }
}
