using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterGenerator : MonoBehaviour
{
    List<MonsterController> monsters = new List<MonsterController>();
    List<string> monsterNames = new List<string> { "Goblin", "Mushroom", "Skeleton" };
    [SerializeField] GameObject goal;
    [SerializeField] Transform goal_pos;
    bool check = false;
  
    // Start is called before the first frame update
    void Start()
    {      
        foreach (MonsterController go in Resources.FindObjectsOfTypeAll(typeof(MonsterController)) as MonsterController[])
        {
            string monName = go.GetComponent<MonsterController>().monsterName;
            if (monsterNames.FindIndex(x => x.Equals(monName))>=0)
                monsters.Add(go);            
        }      
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
        if (!check&&GameManager.StageNum!=3) 
        {
             check = true;
             Instantiate(goal, goal_pos.position, Quaternion.identity);
        }
    }

    public void StageClear()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].isDead = true;
            Destroy(monsters[i].gameObject);
        }
    }
}
