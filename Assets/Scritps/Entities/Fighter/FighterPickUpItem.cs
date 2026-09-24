using System;
using UnityEngine;

public class FighterPickUpItem : MonoBehaviour
{
    [Header("Weapon Slot")]
    [SerializeField] private GameObject weaponSlot;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            var weapon = other.GetComponent<WeaponController>();
            if (weapon.IsOccupied()) return;
            
            Debug.Log(weapon.gameObject.name);
            if(weaponSlot.transform.childCount > 0) Destroy(weaponSlot.transform.GetChild(0).gameObject);
            
            weapon.transform.SetParent(weaponSlot.transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            
            weapon.SetOwner(gameObject);
        }
    }
}
