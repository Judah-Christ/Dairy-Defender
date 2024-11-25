using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _attackDmg;


    public void StartAttacking(Transform target)
    {
        StartCoroutine(ConstantAttack(target));
    }
    public void StopAttacking(Transform target)
    {
        StopCoroutine(ConstantAttack(target));
    }
    public IEnumerator ConstantAttack(Transform target)
    {
        while (true && target != null)
        {
            target.GetComponent<ObjectiveManager>().TakeDamage(_attackDmg);
            yield return new WaitForSeconds(2f);
        }
    }
}
