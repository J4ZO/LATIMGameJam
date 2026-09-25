using System;
using System.Collections;
using UnityEngine;

public class FighterStunned : MonoBehaviour
{
    [Header("Stunned")] 
    [SerializeField] private float stunnedTime;
    [SerializeField] private bool isStunned;
    
    [Header("Resistance")]
    [SerializeField] private float resistance;
    [SerializeField] private float maxResistance;

    private void Awake()
    {
        resistance = maxResistance;
    }

    public void Stunned(float damage)
    {
        if (isStunned) return;
        
        resistance -= damage;
        
        if (resistance <= 0)
        {
            StartCoroutine(StunnedCoroutine());
        }
    }
    
    
    private IEnumerator StunnedCoroutine()
    {
        isStunned = true;
        yield return new WaitForSeconds(stunnedTime);
        isStunned = false;
        resistance = maxResistance;
    }

    public bool IsStunned()
    {
        return isStunned;
    }
    
}
