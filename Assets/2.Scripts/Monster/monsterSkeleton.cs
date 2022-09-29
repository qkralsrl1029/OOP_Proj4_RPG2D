using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterSkeleton : MonsterController
{
    private int def = 10;
    private bool isInSkill = false;
    private float skillTime = 2f;       //스킬 지속 시간

    // Start is called before the first frame update
    void Awake()
    {
        init("Skeleton", 10, 1, 7, 0.5f, 2);
        InvokeRepeating("movementChange", 2f, 2.3f);    //행동상태 일정 주기마다 반복
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        ActionSet();    //행동상태에 따라 일정한 행동 반복
        detectPlayer();
        player = FindObjectOfType<characterController>();
    }

    public override void MonsterSkill()
    {
        base.MonsterSkill();

        //회복 스킬
        monsterHp *= 2;
        def *= 2;
        isInSkill = true;
        StartCoroutine(SkillUse());
    }

    IEnumerator SkillUse()
    {
        yield return new WaitForSeconds(skillTime);
        isInSkill = false;
    }
}
