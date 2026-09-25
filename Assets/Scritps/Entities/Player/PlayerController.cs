using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");
    private static readonly int Attacked = Animator.StringToHash("Attacked");
    private static readonly int IsStunned = Animator.StringToHash("IsStunned");
    private static readonly int StunnedEnter = Animator.StringToHash("Stunned");

    [Header("References")]
    private PlayerMovement _playerMovement;
    private FighterAttack _fighterAttack;
    private FighterStunned _fighterStunned;
    private Animator _animator;

    [Header("Input Actions")] 
    [SerializeField] private InputActionReference movementAction;
    [SerializeField] private InputActionReference runAction;
    [SerializeField] private InputActionReference attackAction;

    [Header("Variables")] 
    private Vector2 _direction;
    public bool _wasStunned;
    public bool _isStunned;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _fighterAttack = GetComponent<FighterAttack>();
        _fighterStunned = GetComponent<FighterStunned>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        attackAction.action.Enable();
        attackAction.action.performed += Attack;
    }

    private void OnDisable()
    {
        attackAction.action.performed -= Attack;
        attackAction.action.Disable();
    }

    private void Update()
    {
        _isStunned = Stunned();
        if (_isStunned) return;
        
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isStunned) return;
        
        _direction = movementAction.action.ReadValue<Vector2>();
        _playerMovement.Movement(_direction);
        
        Vector2 pos = Mouse.current.position.ReadValue();
        _playerMovement.Rotate(pos);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (_fighterAttack.IsCooldown() || _fighterStunned.IsStunned()) return;
        
        _fighterAttack.Attack();
        _animator.SetTrigger(Attacked);
    }

    private bool Stunned()
    {
        bool isStunned = _fighterStunned.IsStunned();
        if (isStunned == _wasStunned) return isStunned;
        
        Debug.Log("Is Stunned = " + isStunned);
        _animator.SetBool(IsStunned, isStunned);
        if (isStunned)
        {
            _animator.SetBool(IsRunning, false);
            _animator.SetBool(IsWalking, false);
            _animator.SetTrigger(StunnedEnter);
        }

        _wasStunned = isStunned;
        return isStunned;
    }
}
