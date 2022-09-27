using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] float attackRange;
    [SerializeField] LayerMask layer;
    [SerializeField] int Damage;

    public void DoAttack()  //애니메이션 이벤트 호출
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, attackRange, layer);

        if (ray)
        {
            ray.transform.GetComponent<MonsterController>().GetAttacked(Damage);
        }

    }
}
