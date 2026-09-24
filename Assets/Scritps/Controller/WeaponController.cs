using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private GameObject owner;

    [Header("Variables")] [SerializeField] private bool isOccupied;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") && other.gameObject != owner) || (other.CompareTag("Enemy") && other.gameObject != owner))
        {
            Debug.Log("Damage");
        }
    }

    public void SetOwner(GameObject entity)
    {
        isOccupied = true;
        owner = entity;
        GetComponent<SyntyWaterBobGoblin>().enabled = false;
    }
    
    public bool  IsOccupied()
    {
        return isOccupied;
    }
}
