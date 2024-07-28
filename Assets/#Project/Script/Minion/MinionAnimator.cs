using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAnimator : MonoBehaviour
{
    public Animator animator;
    Minion minion;
    // Start is caled before the first frame update
    void Awake()
    {
        minion = GetComponent<Minion>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        Debug.Log($" minion animator{minion.name}");
    }

    // Update is called once per frame
    void Update()
    {
        if(minion.state == MinionState.Walk) animator.SetTrigger("Walk");
        if(minion.state == MinionState.Attacking) animator.SetTrigger("Attack");
        if(minion.state == MinionState.Damaged) animator.SetTrigger("Damaged");
        if(minion.state == MinionState.Destroy) animator.SetTrigger("Death");
    }
}
