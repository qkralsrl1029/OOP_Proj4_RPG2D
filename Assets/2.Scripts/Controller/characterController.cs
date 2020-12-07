using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterController : MonoBehaviour
{
    Animator anim;
    [SerializeField] SpriteRenderer characterImage;
    [SerializeField] Sprite[] jumpImgs;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float attackRange;
    [SerializeField] int Damage;
    [SerializeField] int SkillDamage;
    [SerializeField] GameObject skillEffect;
    int FinalDmg;
    [SerializeField] int maxHp;
    [SerializeField] int maxMp;
    int Hp;
    int Mp;
    LayerMask layer = 1 << 9;


    [SerializeField] float attackCoolTime;
    [SerializeField] float skillCoolTime;

    [SerializeField] Image hpBar;
    [SerializeField] GameObject Status;
   

    Rigidbody2D rigid;
    bool isjump = false;
    bool isGrounded = true;
    bool isAttack = false;
    bool isDead = false;
   

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        characterImage = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Hp = maxHp;
        Mp = maxMp;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        tryJump();
        attack();
        if(!isAttack)
            move();
        
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
        if(Input.GetKeyDown(KeyCode.W))
        {
            rigid.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, jumpPower*2);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
        }

    }

    void CheckGrounded()    //땅에 닿았는지를 체크하여 애니메이션 실행, 점프/추락
    {
        if (Physics2D.Raycast(transform.position, -transform.up, 1.1f, 1 << 10))    //땅에 닿았을 경우
        {
            isGrounded = true;
            isjump = false;
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
        }
        else    //땅에 닿지 않았을때
        {
            isGrounded = false;
            isjump = true;
            if (rigid.velocity.y > 0)   //속도가 0이상이면(올라가는중이면)
            {
                //점프 애니메이션 실행
                anim.SetBool("Falling", false);
                anim.SetBool("Jumping", true);

            }
            else    //속도가 0이하면(떨어지는 중이면)
            {
                //떨어지는 애니메이션 실행
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
    }

    void jump()
    {
        //스페이스 키가 눌렸을때 점프중이 아니라면
        if (!isjump)
            return;

        //+Y방향으로 addforce
        rigid.velocity = Vector2.zero;
        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);       
    }

    void attack()   //공격중이 아닐때만 공격 가능
    {
        //if(Input.GetKeyDown(KeyCode.LeftControl)&&isCombo)
        //    StartCoroutine(DoAttack("2"));
        int randomAttack = UnityEngine.Random.Range(1, 3);
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttack && !isjump)
        {
            FinalDmg = Damage;
            StartCoroutine(DoAttack(randomAttack.ToString()));
        }
        if (Input.GetKeyDown(KeyCode.Q) && !isAttack && !isjump)
        {
            StartCoroutine(DoSkill());
            FinalDmg = SkillDamage;
        }
    }
    IEnumerator DoAttack(string comboIndex)
    {
        isAttack = true;
        AudioManager.instance.PlaySFX("Attack");
        anim.SetTrigger("Attack"+comboIndex);                          //애니메이션 실행 도중 공격 함수 호출 
        yield return new WaitForSeconds(attackCoolTime);    //공격 딜레이만큼 기다림
        isAttack = false;
    }

    IEnumerator DoSkill()
    {
        isAttack = true;
        anim.SetTrigger("Skill");
        yield return new WaitForSeconds(skillCoolTime);
        isAttack = false;
    }

    void SkillEffect()
    {
        GameObject go = Instantiate(skillEffect,transform.position+transform.right*2f,Quaternion.identity);
        AudioManager.instance.PlaySFX("Skill");
        Destroy(go, 0.5f);
    }

    public void GiveDamage()  //애니메이션 이벤트 호출
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, attackRange, layer);

        if (ray)
        {
            ray.transform.GetComponent<MonsterController>().GetAttacked(FinalDmg);
        }

    }


    public void getDamage(int damage)       //피격 함수
    {
        if (!isAttack)
        {
            anim.SetTrigger("GetHit");
            //AudioManager.instance.PlaySFX("GetHit");
            Hp -= damage;
            hpBar.fillAmount = (float)Hp / maxHp;       //fillamount는 0부터 1까지의 값만 가지므로 실수형으로 형 변환
            if(Hp<=0&& !isDead)
            {
                isDead = true;
                anim.SetBool("isDead", true);
                anim.SetTrigger("Die");
                GameManager.StageNum--;
                GameManager.instance.ChangeScene();
                AudioManager.instance.PlaySFX("Respawn");
            }
        }
    }
    
    

    //몬스터 머리위에서도 점프가능하게

    private void OnCollisionEnter2D(Collision2D collision)  //땅에 닿았을때 다시 점프 가능
    {
        if (collision.transform.tag == "Ground" || collision.transform.tag == "Monster")
        {
            isGrounded = true;
            isjump = false;
        }
    }

    public void GameSet()
    {
        Destroy(Status);
    }
}
