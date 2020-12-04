using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterMushroom : MonsterController
{
    // Start is called before the first frame update
    void Start()
    {
        init("Mushroom", 10, 1, 1, 0.3f, 3f);
        InvokeRepeating("movementChange", 2f, 2.3f);    //행동상태 일정 주기마다 반복
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isDead)
            return;
        ActionSet();    //행동상태에 따라 일정한 행동 반복
        Dead();
        detectPlayer();
    }
}