using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public enum PlacementMode
{
    Fixed,
    Valid,
    Invalid
}
[RequireComponent(typeof(Building))]
public class BuildingManager : MonoBehaviour
{
    public Material validPlacementMaterial;
    public Material invalidPlacementMaterial;

    public MeshRenderer[] meshComponents;
    private Dictionary<MeshRenderer, List<Material>> initialMaterials;

    [HideInInspector] public bool hasValidPlacement;
    public bool isFixed;
    private int _nObstacles;
    private Building building;
    public Collider buildingCollider;
    private bool enemyZone = false;
    private bool baseZone = false;
    DisplayManager displayManager;

    public UnityEvent whenIsBuild;


    private void Awake()
    {
        building = GetComponent<Building>();
        _nObstacles = 0;
        displayManager = GameObject.FindObjectOfType<DisplayManager>();
        if (displayManager == null) Debug.Log("NOT FOUND");
        if (building.isEnemy) isFixed = true;
        if (!building.isEnemy && !isFixed)
        {
            _InitializeMaterials();
            SetPlacementMode(PlacementMode.Valid);
        }

    }

    void Update()
    {
        if (!building.isEnemy && !isFixed)
        {
            if (CanBeBuild() && _nObstacles == 0 && !hasValidPlacement)
            {
                SetPlacementMode(PlacementMode.Valid);
            }
            else if ((!CanBeBuild() || _nObstacles > 0) && hasValidPlacement)
            {
                SetPlacementMode(PlacementMode.Invalid);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} - {other.tag}");
        if(other.CompareTag("Attack Zone")) return;
        if (isFixed) return;
        if (other.CompareTag("EnemyZone"))
        {
            enemyZone = true;
        }
        else if (other.CompareTag("BaseZone"))
        {
            baseZone = true;
        }
        // ignore ground objects
        else if (_IsGround(other.gameObject))
        {
            return;
        }
        else
        {
            _nObstacles++;
        Debug.Log(_nObstacles);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Attack Zone")) return;
        if (isFixed) return;
        if (other.CompareTag("EnemyZone")) enemyZone = false;
        else if (other.CompareTag("BaseZone")) baseZone = false;
        // ignore ground objects
        else if (_IsGround(other.gameObject)) return;
        else _nObstacles--;
        // if (_nObstacles == 0)
        //     SetPlacementMode(PlacementMode.Valid);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _InitializeMaterials();
    }
#endif

    public void SetPlacementMode(PlacementMode mode)
    {
        if (mode == PlacementMode.Fixed)
        {
            isFixed = true;
            hasValidPlacement = true;
            SetUpFacotryAndBuilding();
        }
        else if (mode == PlacementMode.Valid)
        {
            hasValidPlacement = true;
        }
        else
        {
            hasValidPlacement = false;
        }
        SetMaterial(mode);
    }



    public void SetMaterial(PlacementMode mode)
    {
        if (mode == PlacementMode.Fixed)
        {
            foreach (MeshRenderer r in meshComponents)
                r.sharedMaterials = initialMaterials[r].ToArray();
        }
        else
        {
            Material matToApply = mode == PlacementMode.Valid && CanBeBuild() ? validPlacementMaterial : invalidPlacementMaterial;

            Material[] m; int nMaterials;
            foreach (MeshRenderer r in meshComponents)
            {
                nMaterials = initialMaterials[r].Count;
                m = new Material[nMaterials];
                for (int i = 0; i < nMaterials; i++)
                    m[i] = matToApply;
                r.sharedMaterials = m;
            }
        }
    }

    private void _InitializeMaterials()
    {
        if (initialMaterials == null) initialMaterials = new Dictionary<MeshRenderer, List<Material>>();
        if (initialMaterials.Count > 0)
        {
            foreach (var l in initialMaterials) l.Value.Clear();
            initialMaterials.Clear();
        }

        foreach (MeshRenderer r in meshComponents)
        {
            initialMaterials[r] = new List<Material>(r.sharedMaterials);
        }
    }

    private bool _IsGround(GameObject o)
    {
        return ((1 << o.layer) & BuildingPlacer.instance.groundLayerMask.value) != 0;
    }

    private void SetUpFacotryAndBuilding()
    {
        MinionFactory minionFactory = gameObject.GetComponentInChildren<MinionFactory>();
        if (minionFactory != null)
        {
            minionFactory.CanLaunchMinion = true;
            minionFactory.Initialize();
        }
        //builging available to be in setTarget()
        building.isCreated = true;
        PlayerData.wood -= building.WoodCost;
        PlayerData.stone -= building.StoneCost;
        displayManager.UpdatePlayerBoard();
        whenIsBuild?.Invoke();
    }

    public bool CanBeBuild()
    {
        //Check s'il y a assez de ressource
        if (building.WoodCost > PlayerData.wood || building.StoneCost > PlayerData.stone)
        {
            return false;
        }
        //check si dans la zone ennemie
        if (enemyZone) return false;
        //Check Base Zone Rules
        // 1) pas de tour
        if (gameObject.tag == "Tower" && baseZone) return false;
        // 2) building producteur de minion uniquement dans la base Zone
        if (gameObject.tag != "Tower" && !baseZone) return false;
        // 3) un seul type de building a la fois dans la base zone
        if (gameObject.tag != "Tower" && baseZone)
        {
            Building[] buildings = FindObjectsOfType<Building>();
            foreach (var building in buildings)
            {
                if (building.isCreated && !building.isEnemy && gameObject.tag == building.gameObject.tag)
                {
                    return false;
                }
            }
        }
        return true;
    }
}