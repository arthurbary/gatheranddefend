using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public enum PlayerState
{
    Attack,
    Walk,
    Idle,
}
public class PlayerBehaviour : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction attack;
    private WeaponMovement weaponMovement;
    public PlayerState state;
    void Awake() 
    {
        playerInput = GetComponent<PlayerInput>();
        attack = playerInput.actions["PlayerAttack"];
        state = PlayerState.Idle;
    }
    void Start()
    {
        weaponMovement = GameObject.FindWithTag("WeaponBasic").GetComponent<WeaponMovement>();
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
        if(attack.ReadValue<float>() != 0)BasicAttack();
    }
    
    void BasicAttack()
    {
        
        if(weaponMovement.CanRotate())
        {
            state = PlayerState.Attack;
            StartCoroutine(weaponMovement.RotateObject());
        }
    }

    
}
