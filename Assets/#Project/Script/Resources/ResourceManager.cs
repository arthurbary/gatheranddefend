using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] int totalWoodStock;
    [SerializeField] int totalStoneStock;
    [SerializeField] int numberOfShuffle = 10;
    Resource[] resources;
    private List<Resource> woodResources;
    private List<Resource> stoneResources;

    void Start()
    {
        DuplicateAndRotateObject();
        woodResources = new List<Resource>();
        stoneResources = new List<Resource>();
        resources = FindObjectsOfType<Resource>();
        foreach ( Resource resource in resources)
        {

            if(resource.Type == Resource.ResourceType.WOOD)
            {
                woodResources.Add(resource);
            } else
            {
                stoneResources.Add(resource);
            }
        }
        Debug.Log($"all the wood {woodResources.Count} stone {stoneResources.Count}");
        SetUpRessources(woodResources, totalWoodStock);
        SetUpRessources(stoneResources, totalStoneStock);

        RedistributionOfResources(woodResources, numberOfShuffle);
        RedistributionOfResources(stoneResources, numberOfShuffle);
    }

    void SetUpRessources(List<Resource> resources, int totalStock)
    {
        int restOfStock = totalStock % resources.Count != 0 ? totalStock % resources.Count : 0;
        totalStock -= restOfStock;
        int baseResource = totalStock / resources.Count;
        //distribue un amout de base egal pour tous
        foreach (Resource resource in resources) 
        {
            resource.Amount += baseResource;
        }
        //redistribution du reste du stock
        for (int i = 0; i < restOfStock; i++)
        {
            resources[Random.Range(0, resources.Count)].Amount += 1;
        }
    }

    void RedistributionOfResources(List<Resource> resources, int numberOfShuffle)
    {
        for (int i = 0; i < numberOfShuffle; i++)
        {
            foreach (Resource resource in resources)
            {
                if(resource.Amount > 1)
                {
                    int amountToMove = Random.Range(1, resource.Amount);
                    resources[Random.Range(0, resources.Count)].Amount += amountToMove;
                    resource.Amount -= amountToMove;
                }
            }
        }
    }
    void DuplicateAndRotateObject()
    {
        GameObject objectToDuplicate = GameObject.Find("HalfRessources");
        if (objectToDuplicate != null)
        {
            GameObject duplicate = Instantiate(objectToDuplicate, objectToDuplicate.transform.position, objectToDuplicate.transform.rotation);
            duplicate.transform.Rotate(0, 180, 0);
        }
    }
}

