using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    /* 
    TOTAL ne peut pas etre plus grand que 1 
    SANS OUBLIER LES RESSOURCES QUI NE SONT EN RESIDUELE 
    */
    [SerializeField] public static float BaseRate = 0.6f;
    [SerializeField] public static float TowerRate = 0.2f;
    [SerializeField] public static float OtherBuildingRate = 0.1f;
    // RESSOURCE : 0.1f
}
