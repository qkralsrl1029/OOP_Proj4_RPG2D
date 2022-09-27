using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterDetection : MonoBehaviour   //몬스터의 플레이어 감지 범위
{
    [SerializeField] MonsterController monster;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name=="Player"&& !monster.isDead)
        {
            monster.Attack();
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player" && !monster.isDead)
            monster.CancelAttack();
    }
}
