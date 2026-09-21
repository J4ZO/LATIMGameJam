using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothRotation;
    private float _yaw, _targetYaw, _yawVelocity;

    [Header("References")] 
    private Rigidbody _rb;

    [SerializeField] private Camera cameraRef;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _yaw = _targetYaw = transform.eulerAngles.y;
    }

    public void Movement(Vector2 dir)
    {
        Vector3 direction = new Vector3(dir.x, 0, dir.y);
        _rb.MovePosition(_rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
    }

    public void Rotate(Vector2 lookDelta)
    {
        Vector3 target = cameraRef.ScreenToWorldPoint(lookDelta);
        
        float angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);

        float angleGrades = (180 / Mathf.PI) * angle - 90;
        float smooth = Mathf.SmoothDampAngle(_yaw, _targetYaw, ref _yawVelocity, 0.1f);
        
        _rb.MoveRotation(Quaternion.Euler(0f, 0f, angleGrades));
    }
}
