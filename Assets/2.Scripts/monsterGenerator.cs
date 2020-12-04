using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterGenerator : MonoBehaviour
{
    public MonsterController[] monsterPrefab = new MonsterController[2];
    List<MonsterController> monsters = new List<MonsterController>();
    public GameObject goal;
    public int[] count = new int[2];

    [SerializeField] Transform[] monster_pos;
    [SerializeField] Transform goal_pos;
    int check = 0;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < count[0]; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[0], monster_pos[i].position, Quaternion.identity);
            monsters.Add(go);
        }
        for (int i = 2; i < count[1] + 2; i++)
        {
            MonsterController go = Instantiate(monsterPrefab[1], monster_pos[i].position, Quaternion.identity);
            monsters.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < 6; i++)
        {
            check = 0;
            if (!monsters[i].isDead) {
                check = 1;
                break;
            }
        }
        if (check == 0) {   
             Instantiate(goal, goal_pos.position, Quaternion.identity);
        }
    }
}
