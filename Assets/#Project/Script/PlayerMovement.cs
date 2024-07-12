using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    private bool isMoving;
    private float lastClick;
    [SerializeField] float timeBetweenClick;



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //Input.GetMouseButton(0);
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            lastClick = Time.time;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isMoving = false;
            if(Time.time - lastClick > timeBetweenClick)
            {
                agent.SetDestination(agent.transform.position);
            }
            else
            {
                GoToMouse();
            }
        }
        if(isMoving)GoToMouse();
    }

    void GoToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction*100, Color.green);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            agent.SetDestination(hit.point);
        }
    }
}
