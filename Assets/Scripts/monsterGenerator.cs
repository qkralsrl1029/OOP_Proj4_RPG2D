using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterGenerator : MonoBehaviour
{
    [SerializeField] GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(monster, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
