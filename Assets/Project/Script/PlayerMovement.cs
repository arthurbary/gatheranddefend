using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;

    // For double-click detection
    private float lastClickTime = 0;
    private float doubleClickThreshold = 0.3f; // Time interval to detect double-click


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
                GoToMouse();
        }
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
