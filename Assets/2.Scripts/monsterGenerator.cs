using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterGenerator : MonoBehaviour
{
    [SerializeField] MonsterController[] monsterPrefab = new MonsterController[4];
    List<MonsterController> monsters = new List<MonsterController>();
    [SerializeField] GameObject goal;

    [SerializeField] int[] count = new int[4];
    
    [SerializeField] Transform[] monster_pos;
    [SerializeField] Transform goal_pos;
    bool check = false;
    // Start is called before the first frame update
    void Start()
    {
         
        //count[0,0] =2 goblin
        for (int i = 0; i < count[0]; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[0], monster_pos[i].position, Quaternion.identity);
            monsters.Add(go);
        }
        //count[0,1] =4 skeleton
        for (int i = 0; i < count[1]; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[1], monster_pos[i + count[0]].position, Quaternion.identity);
            monsters.Add(go);
        }
        //count[0,2] = 0 mushroom
        for (int i = 0; i < count[2] ; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[2], monster_pos[i + count[0] + count[1]].position, Quaternion.identity);
            monsters.Add(go);
        }
        //count[0,2] = 0 boss
        for (int i = 0; i < count[3]; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[3], monster_pos[i + count[0] + count[1] + count[2]].position, Quaternion.identity);
            monsters.Add(go);
        }

        //monster_pos 6번까지 사용
    }
    
    public void createMonster()
    {
        /*
        int stagenum = GameManager.StageNum;

        DontDestroyOnLoad(gameObject);
        //count[0,0] =2 goblin
        for (int i = 0; i < count[stagenum, 0]; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[0], monster_pos[i].position, Quaternion.identity);
            monsters.Add(go);
        }
        //count[0,1] =4 skeleton
        for (int i = 0; i < count[stagenum, 1]; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[1], monster_pos[i + count[0, 0]].position, Quaternion.identity);
            monsters.Add(go);
        }
        //count[0,2] = 0 mushroom
        for (int i = 0; i < count[stagenum, 2]; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[2], monster_pos[i + count[0, 0] + count[0, 1]].position, Quaternion.identity);
            monsters.Add(go);
        }
        //count[0,2] = 0 boss
        for (int i = 0; i < count[stagenum, 3]; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[3], monster_pos[i + count[0, 0] + count[0, 1] + count[0, 2]].position, Quaternion.identity);
            monsters.Add(go);
        }
        */
    }
    

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < monsters.Count ; i++)
        {           
            if (!monsters[i].isDead) {
                return;
            }
        }
        if (!check) 
        {
             check = true;
             Instantiate(goal, goal_pos.position, Quaternion.identity);
        }
    }


    
}
