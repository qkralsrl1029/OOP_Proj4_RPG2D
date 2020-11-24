using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterGoblin : MonsterController
{
    // Start is called before the first frame update
    void Start()
    {
        init("Goblin",100,1,1,1,1);
        InvokeRepeating("movementChange", 2f, 2.3f);    //행동상태 일정 주기마다 반복
    }

    // Update is called once per frame
    void Update()
    {
        ActionSet();    //행동상태에 따라 일정한 행동 반복
        Dead();
    }


}
