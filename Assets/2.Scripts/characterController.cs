using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer characterImage;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] int maxHp;
    [SerializeField] int maxMp;
    int Hp;
    int Mp;


    [SerializeField] float attackCoolTime;

    [SerializeField] Image hpBar;
    [SerializeField] Image mpBar;
   

    Rigidbody2D rigid;
    bool isjump = false;
    bool isGrounded = true;
    bool isAttack = false;
   

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        Hp = maxHp;
        Mp = maxMp;
    }

    // Update is called once per frame
    void Update()
    {
        tryJump();
        attack();
        move();
        
    }

    private void LateUpdate()
    {
        
    }

    void move()
    {
        Vector3 _move = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (transform.rotation.eulerAngles.y < 100)
                transform.Rotate(0, 180, 0);
            _move = Vector3.right;
            anim.SetBool("Move", true);          
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (transform.rotation.eulerAngles.y > 100)
                transform.Rotate(0, 180, 0);
            _move = Vector3.right;
            anim.SetBool("Move", true);
        }
        else
            anim.SetBool("Move", false);
        transform.Translate(_move * moveSpeed * Time.deltaTime);
    }

    void tryJump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                isjump = true;
                jump();
            }
        }
    }

    void jump()
    {
        if (!isjump)
            return;

        anim.SetTrigger("Jump");
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);       
    }

    void attack()   //공격중이 아닐때만 공격 가능
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttack)
            StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        isAttack = true;
        anim.SetTrigger("Attack");                          //애니메이션 실행 도중 공격 함수 호출 
        yield return new WaitForSeconds(attackCoolTime);    //공격 딜레이만큼 기다림
        isAttack = false;
    }

    public void getDamage(int damage)       //피격 함수
    {
        Hp -= damage;
        hpBar.fillAmount =(float) Hp / maxHp;       //fillamount는 0부터 1까지의 값만 가지므로 실수형으로 형 변환
    }
    

    private void OnCollisionEnter2D(Collision2D collision)  //땅에 닿았을때 다시 점프 가능
    {
        if (collision.transform.tag == "Ground")
        {
            isGrounded = true;
            isjump = false;
        }
    }
}
