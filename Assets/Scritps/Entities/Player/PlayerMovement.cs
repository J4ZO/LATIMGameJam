using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float smoothSpeed;
    private float _initialSpeed;
    private float _targetSpeed;

    
    [Header("References")] 
    private Rigidbody _rb;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Camera cameraRef;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _initialSpeed = moveSpeed;
    }

    public void Movement(Vector2 dir)
    {
        Vector3 direction = new Vector3(dir.x, 0, dir.y);
        _rb.MovePosition(_rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
    }

    public void Rotate(Vector2 pointer)
    {
        Vector3 mousePos = new Vector3(pointer.x, pointer.y, 0);
        
        var (success, position) = GetMousePosition(mousePos);

        if (success)
        {
            var dir = position - transform.position;

            dir.y = 0f;
            
            Quaternion rotation = Quaternion.LookRotation(dir);
            _rb.MoveRotation(rotation);
        }
    }

    private (bool success, Vector3 position) GetMousePosition(Vector3 mousePosition)
    {
        var ray = cameraRef.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
            return (success: true, position: hit.point);
        }
        
        return (success: false, position: Vector3.zero);
        
    }

    public void SetSpeed(float newSpeed)
    {
        _targetSpeed =  newSpeed;
    }
    public void SetSpeed()
    {
        _targetSpeed =runSpeed;
    }

    public void ResetSpeed()
    {
        _targetSpeed = _initialSpeed;
    }

    public void ChangeSpeed()
    {
        if (Mathf.Approximately(moveSpeed, _targetSpeed)) return;
        
        
        moveSpeed = Mathf.Lerp(moveSpeed, _targetSpeed,  smoothSpeed * Time.deltaTime);
    }
    
}
