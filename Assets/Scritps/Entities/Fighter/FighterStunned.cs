using System;
using System.Collections;
using UnityEngine;

public class FighterStunned : MonoBehaviour
{
    private static readonly int Hit = Animator.StringToHash("Hit");

    [Header("Stunned")] 
    [SerializeField] private float stunnedTime;
    [SerializeField] private bool isStunned;
    
    [Header("Resistance")]
    [SerializeField] private float resistance;
    [SerializeField] private float maxResistance;


    [Header("References")]
    private Animator _anim;
    private void Awake()
    {
        resistance = maxResistance;
        _anim = GetComponent<Animator>();
    }

    public void Stunned(float damage)
    {
        if (isStunned) return;

        resistance -= damage;
        
        if (resistance <= 0)
        {
            StartCoroutine(StunnedCoroutine());
        }
        else
        {
            _anim.SetTrigger(Hit);
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
