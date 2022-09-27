using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterBoss : MonsterController
{
    [SerializeField] GameObject attackTrigger;

    // Start is called before the first frame update
    void Start()
    {
        init("Boss", 500, 1, 10, 3f, 2f);
        InvokeRepeating("movementChange", 2f, 2.3f);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            ActionSet();    //행동상태에 따라 일정한 행동 반복
            detectPlayer();
            
        }
        EndGame();
    }

    protected override void CheckRange()
    {
        if (!isAttacking)
            StartCoroutine(DoAttack());
    }

    protected override IEnumerator DoAttack()
    {
        attackTrigger.SetActive(true);
        isAttacking = true;
        anim.SetTrigger("attack");  //공격 애니메이션 실행 후
        yield return new WaitForSeconds(attackSpeed);   //공격속도만큼 대기
        isAttacking = false;
        attackTrigger.SetActive(false);
    }

    void EndGame()
    {
        if(mosnterHp<=0&& !GameManager.isEnd)
        {
            GameManager.isEnd = true;
            Time.timeScale = 0.3f;
            GameManager.instance.ChangeScene();
            player.GetComponent<characterController>().GameSet();
        }
    }
}
