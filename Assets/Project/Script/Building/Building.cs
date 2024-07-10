using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public enum BuildingType
    {
        BASE = 1,
        BLACKSMITH = 2,
        GYM = 3,
        LAB = 4,
        TOWER = 5,
        ROAD = 6

    }
    protected int WoodCost { get; set; }
    protected int StoneCost { get; set; }
    protected int Level { get; set; }
    protected int Life { get; set; }
    protected BuildingType Type { get; set; }
    private bool isUnderAttack = false;
    public bool isEnemy = false;

    protected IEnumerator _BeingAttack()
    {
        isUnderAttack = true;
        if(Life >= 0){
            Life--;
            /* 
            ???Repaire the Building???
            WoodCost--;
            StoneCost--; 
            */
        } 
        else 
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.1f);
        isUnderAttack = false;
    }
    private void BeingAttack()
    {
        if(isUnderAttack) return;
        StartCoroutine(_BeingAttack());
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("WeaponBasicHit"))
        {
            BeingAttack();
            /* Debug.Log("Resource Type: " + Type);
            Debug.Log("Resource Life: " + Life);
            Debug.Log("Resource Amount: " + Amount);
            Debug.Log("Player Wood: " + PlayerData.wood);
            Debug.Log("Player Stone: " + PlayerData.stone); */
        }
    }
}
