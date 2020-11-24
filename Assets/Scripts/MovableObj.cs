using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObj : MonoBehaviour
{
    protected float moveSpeed;

    protected Rigidbody2D rigid;

    protected void move()
    {
        transform.Translate(transform.right * moveSpeed * Time.deltaTime);
    }

    protected void init(float _speed)
    {
        moveSpeed = _speed;
    }
    
}
