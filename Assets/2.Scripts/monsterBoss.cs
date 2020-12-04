using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterBoss : MonsterController
{
    // Start is called before the first frame update
    void Start()
    {
        init("Boss", 500, 1, 1, 0.3f, 2f);
        InvokeRepeating("movementChange", 2f, 2.3f);
    }

    // Update is called once per frame
    void Update()
    {
        ActionSet();    //행동상태에 따라 일정한 행동 반복
        Dead();
        detectPlayer();
    }
}
