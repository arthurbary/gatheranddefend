using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    public enum ResourceType
    {
        STONE = 1,
        WOOD = 2
    }
    public int Life { get; set; }
    public int Amount { get; set; }
    public int Subtractor { get; set; }
    public ResourceType Type { get; set; }

    private bool isRunning = false;
    private WeaponMovement weaponMovement;
        void Start()
    {
        weaponMovement = GameObject.FindWithTag("WeaponBasic").GetComponent<WeaponMovement>();
    }
    protected virtual IEnumerator _GiveResource()
    {
        isRunning = true;
        if(Life >= 0)
        {
            Amount -= Subtractor;
            if (Type == ResourceType.STONE) 
            {
                PlayerData.stone += Subtractor;
            }
            if (Type == ResourceType.WOOD) 
            {
                PlayerData.wood += Subtractor;
            }
            Life--;
        } 
        else
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.1f);
        isRunning = false;
    }


    private void GiveResource()
    {
        if(isRunning) return;
        StartCoroutine(_GiveResource());
    }
    public void GenerateLifeAndAmount()
    {
        Debug.Log("GENERATE");
        System.Random rand = new();
        int randomNumber = rand.Next(0, 100);
        Life = randomNumber;
        Amount = Life * Subtractor;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("WeaponBasicHit") && weaponMovement.isCooldownActive)
        {
            GiveResource();
            Debug.Log("Resource Type: " + Type);
            Debug.Log("Resource Life: " + Life);
            Debug.Log("Resource Amount: " + Amount);
            Debug.Log("Player Wood: " + PlayerData.wood);
            Debug.Log("Player Stone: " + PlayerData.stone);
        }
    }
}
