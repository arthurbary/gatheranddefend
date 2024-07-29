using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    PlayerBehaviour playerBehaviour;
    void Awake()
    {
        playerBehaviour = GetComponent<PlayerBehaviour>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        Debug.Log($" minion animator{playerBehaviour.name}");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerBehaviour.state == PlayerState.Walk) animator.SetTrigger("Walk");
        //if(playerBehaviour.state == PlayerState.Attack) animator.SetTrigger("Attack");
        if(playerBehaviour.state == PlayerState.Idle) animator.SetTrigger("Idle");
    }
}
