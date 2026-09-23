using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int Attacked = Animator.StringToHash("Attacked");

    [Header("References")]
    private PlayerMovement _playerMovement;
    private Animator _animator;

    [Header("Input Actions")] 
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference runAction;
    [SerializeField] private InputActionReference attackAction;

    [Header("Variables")] 
    private Vector2 _direction;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Update()
    {
        
        if(runAction.action.IsPressed() && _direction != Vector2.zero)
        {
            _playerMovement.SetSpeed();
            _animator.SetBool(IsRunning, true);
        }
        else
        {
            _playerMovement.ResetSpeed();
            _animator.SetBool(IsRunning, false);
        }
        
        _animator.SetBool(IsWalking, _direction != Vector2.zero);


        if (attackAction.action.WasPressedThisFrame()) Attack();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _direction = movementAction.action.ReadValue<Vector2>();
        _playerMovement.Movement(_direction);
        
        Vector2 pos = Mouse.current.position.ReadValue();
        _playerMovement.Rotate(pos);
    }

    private void Attack()
    {
        _animator.SetTrigger(Attacked);
    }
}
