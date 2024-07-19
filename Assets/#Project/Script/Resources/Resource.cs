using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    /* 
    reflexion 
    la vie de l'arbre ne depand que de la quantite qu'il reprensente
    lorsqu'un minion enemy attaque l'arbre il lui enleve des ressource
    */
    public enum ResourceType
    {
        STONE = 1,
        WOOD = 2
    }
    public int Life { get; set; }
    public int Amount { get; set; }
    public int Subtractor { get; set; }
    public ResourceType Type { get; set; }
    public WeaponMovement WeaponMovement { get; set; }
    public bool isCreated = true;

    private bool isRunning = false;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("WeaponBasic") && WeaponMovement.isCooldownActive)
        {
            GiveResource();
        }
    }
    protected virtual IEnumerator _GiveResource()
    {
        Debug.Log("it give resource");
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
        //Changer la function c'est l'amout qui va definir la vie de la resource
        System.Random rand = new();
        int randomNumber = rand.Next(0, 100);
        if(Life == 0)Life = randomNumber;
        Amount = Life * Subtractor;
    }
    public void TakeDamage(int damage)
    {
        if(Life >= 0)
        {
            Life -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}