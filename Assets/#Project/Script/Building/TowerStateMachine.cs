using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerState
{
    Wait,
    Firing,
    Cooldown,
    Destroy
}
public class TowerStateMachine : MonoBehaviour
{
    [SerializeField] public int damage = 1;
    [SerializeField] public float damageRate = 1.0f;
    public ArrowFactory arrowFactory;
    public List<Minion> minionToKill;
    public TowerState state;
    private Building tower;
    private float cooldownTimer;
    
    void Start()
    {
        tower = transform.parent.GetComponent<Building>();
        state = TowerState.Wait;
        arrowFactory = transform.parent.GetComponentInChildren<ArrowFactory>();
    }

    void Update()
    {
        if(tower.Life <= 0) state = TowerState.Destroy;
        else if (state == TowerState.Wait) Wait();
        else if (state == TowerState.Firing) Firing();
        else if (state == TowerState.Cooldown) Cooldown();
    }

    void OnTriggerEnter(Collider other)
    {
        Minion otherMinion = other.GetComponent<Minion>();

        if( otherMinion != null && !minionToKill.Contains(otherMinion) && otherMinion.isEnemy != tower.isEnemy && other.gameObject.activeSelf)
        {
            if(otherMinion.attackingTower != null)
            {
                otherMinion.attackingTower.minionToKill.Remove(otherMinion);
            }
            otherMinion.attackingTower = this;
            minionToKill.Add(otherMinion);
        }
    }

    void OnTriggerExit(Collider other) 
    {
        Minion otherMinion = other.GetComponent<Minion>();
        if(otherMinion != null && minionToKill.Count > 0 && minionToKill.Contains(otherMinion) && other.gameObject.activeSelf)
        {
            otherMinion.attackingTower = null;
            minionToKill.Remove(otherMinion);
        }
    }
    void Wait()
    {
        
        if(tower.isCreated && minionToKill.Count > 0) state = TowerState.Firing;
    }

    void Firing()
    {
        if(minionToKill.Count > 0 && minionToKill[0].gameObject.activeSelf)
        {
            StartCoroutine(arrowFactory.Create());
        }
        StartCooldown();
        {}
    }
    void StartCooldown()
    {
        state = TowerState.Cooldown;
        cooldownTimer = damageRate;
    }

    void Cooldown()
    {
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer <= 0)
        {
            state = TowerState.Wait;
        }
    }
}
