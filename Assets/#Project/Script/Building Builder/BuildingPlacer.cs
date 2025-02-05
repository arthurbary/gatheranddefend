using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer instance; // (Singleton pattern)

    public LayerMask groundLayerMask;

    protected GameObject _buildingPrefab;
    protected GameObject _toBuild;

    protected Camera _mainCamera;

    protected Ray _ray;
    protected RaycastHit _hit;
    

    private void Awake()
    {
        instance = this; // (Singleton pattern)
        _mainCamera = Camera.main;
        _buildingPrefab = null;
    }

    private void Update()
    {
        if (_buildingPrefab != null)
        { // if in build mode

            // right-click: cancel build mode
            if (Input.GetMouseButtonDown(2))
            {
                CancelConstruction();
            }

            // hide preview when hovering UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (_toBuild != null &&_toBuild.activeSelf) _toBuild.SetActive(false);
                return;
            }
            else if (_toBuild != null && !_toBuild.activeSelf) _toBuild.SetActive(true);

            // rotate preview with Spacebar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _toBuild.transform.Rotate(Vector3.up, 90);
            }

            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (_toBuild != null && Physics.Raycast(_ray, out _hit, 1000f, groundLayerMask))
            {
                if (!_toBuild.activeSelf) _toBuild.SetActive(true);
                _toBuild.transform.position = _hit.point;

                if (Input.GetMouseButtonDown(1))
                { // if right-click
                    BuildingManager m = _toBuild.GetComponent<BuildingManager>();
                    Building b = m.GetComponent<Building>();
                    if (m.hasValidPlacement)
                    {
                        if(m.CanBeBuild() && m.EnoughResource())
                        {
                            m.SetPlacementMode(PlacementMode.Fixed);
                        }
                        else
                        {
                            CancelConstruction();
                        }
                        // shift-key: chain builds
                        Building building = m.GetComponent<Building>();
                        if (building.CompareTag("Tower") && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                        {
                            _toBuild = null; // (to avoid destruction)
                            _PrepareBuilding();
                        }
                        // exit build mode
                        else
                        {
                            _buildingPrefab = null;
                            _toBuild = null;
                        }
                    }
                    else
                    {
                        CancelConstruction();
                    }
                }

            }
            else if (_toBuild != null && _toBuild.activeSelf) _toBuild.SetActive(false);
        }
    }

    private void CancelConstruction()
    {

        Destroy(_toBuild);
        _toBuild = null;
        _buildingPrefab = null;
        return;
    }

    public void SetBuildingPrefab(GameObject prefab)
    {
        Debug.Log("SetBuildingPrefab");
        _buildingPrefab = prefab;
        _PrepareBuilding();
        EventSystem.current.SetSelectedGameObject(null); // cancel keyboard UI nav
    }

    protected virtual void _PrepareBuilding()
    {
        if (_toBuild) Destroy(_toBuild);

        _toBuild = Instantiate(_buildingPrefab);
        _toBuild.SetActive(false);

        BuildingManager m = _toBuild.GetComponent<BuildingManager>();
        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
    }

}