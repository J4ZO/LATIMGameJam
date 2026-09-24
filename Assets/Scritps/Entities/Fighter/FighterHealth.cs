using System;
using UnityEngine;

public class FighterHealth : MonoBehaviour
{
    [Header("Variables")] 
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    private void Awake()
    {
        health = maxHealth;
    }

    public void OnDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
    
}
