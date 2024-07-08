using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
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
