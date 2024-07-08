using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction attack;
    void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
        attack = playerInput.actions["PlayerAttack"];
    }

    public void OnEnable() 
    {
        attack.Enable();
    }

    public void OnDisable() 
    {
        attack.Disable();
    }
    void Update()
    {
    }
}
