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
    public int Amount { get; set; }
    public int Subtractor { get; set; }
    public ResourceType Type { get; set; }
    public WeaponMovement WeaponMovement { get; set; }
    public bool isCreated = true;

    private bool isRunning = false;
    private DisplayManager displayManager;
    
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
        HitMaker hitMaker = GetComponentInChildren<HitMaker>();
        hitMaker.CreateHit(gameObject);
        if(Amount < Subtractor) Subtractor -= Amount;
        if(Amount > 0)
        {
            Amount -= Subtractor;
            if (Type == ResourceType.STONE) PlayerData.stone += Subtractor;
            if (Type == ResourceType.WOOD) PlayerData.wood += Subtractor;
            if(Amount <= 0)Destroy(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
        displayManager = GameObject.FindObjectOfType<DisplayManager>();
        if(displayManager != null) displayManager.UpdatePlayerBoard();
        yield return new WaitForSeconds(0.1f);
        isRunning = false;
    }
    private void GiveResource()
    {
        if(isRunning) return;
        StartCoroutine(_GiveResource());
    }
    public void TakeDamage(int damage)
    {
        if(Amount >= 0)
        {
            Amount -= damage;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}