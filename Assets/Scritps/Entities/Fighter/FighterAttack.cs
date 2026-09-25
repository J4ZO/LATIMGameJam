using System.Collections;
using UnityEngine;

public class FighterAttack : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float cooldown;
    [SerializeField] private bool isCooldown;
    
    public void Attack()
    {
       StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isCooldown = false;
    }

    public bool IsCooldown()
    {
        return isCooldown;
    }
}
