using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");       
        rigid.AddForce(transform.right * moveX *moveSpeed* Time.deltaTime,ForceMode2D.Force);
    }
}
