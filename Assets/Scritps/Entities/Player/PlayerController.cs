using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement _playerMovement;

    [Header("Input Actions")] 
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference runAction;
    [SerializeField] private InputActionReference rotateAction;
    [SerializeField] private InputActionReference attackAction;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 dir = movementAction.action.ReadValue<Vector2>();
        _playerMovement.Movement(dir);
        
        Vector2 rot = rotateAction.action.ReadValue<Vector2>();
        Vector2 pos = Mouse.current.position.ReadValue();
        _playerMovement.Rotate(pos);
    }
}
