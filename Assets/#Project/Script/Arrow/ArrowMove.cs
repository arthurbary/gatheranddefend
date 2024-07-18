using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] public Transform target;


    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
