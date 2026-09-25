using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponController : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameObject owner;

    [Header("Variables")] 
    [SerializeField] private float damage;
    private bool _isOccupied;

    private void OnTriggerEnter(Collider other)
    {
        if (owner == null || other.gameObject == owner) return;
        
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Debug.Log("Damage");
            var stunned = other.GetComponent<FighterStunned>();
            stunned.Stunned(damage);
        }
    }

    public void SetOwner(GameObject entity)
    {
        _isOccupied = true;
        owner = entity;
        GetComponent<SyntyWaterBobGoblin>().enabled = false;
    }
    
    public bool  IsOccupied()
    {
        return _isOccupied;
    }
}
