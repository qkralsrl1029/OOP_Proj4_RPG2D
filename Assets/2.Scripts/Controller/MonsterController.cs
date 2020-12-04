
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour      //몬스터 동작 컨트롤러
{
    protected string monsterName;  
    protected int mosnterHp;
    protected int maxHp;
    protected float moveSpeed;
    protected float moveDir = 1;
    protected float attackSpeed;
    protected int attackDamage;
    protected float attackRange;

    public bool isDead = false;

    public enum monsterActionType   //몬스터 상태
    {
        Idle=0,
        Move=1,
        Attack,
        Gethit,
        Die,
    }
    protected monsterActionType monsterAction = monsterActionType.Idle;


    protected Rigidbody2D rigid;
    protected Animator anim;
    protected characterController player;

    protected bool isAttacking = false;
    protected bool getHit = false;
    LayerMask layerMask;    //플레이어만 감지하는 레이어마스크
    [SerializeField] Image hpBar;


    protected void init(string _name,int _hp,float _speed,int _damage,float _range,float _atkspeed)
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<characterController>();
        layerMask = 1 << 8;
        hpBar.fillAmount = 1;
        monsterName = _name;
        maxHp = _hp;
        mosnterHp = _hp;
        moveSpeed = _speed;
        attackDamage = _damage;
        attackRange = _range;
        attackSpeed = _atkspeed;
    }

    protected void ActionSet()
    {
        switch (monsterAction)
        {
            case monsterActionType.Idle:    //대기상태
                anim.SetBool("move", false);
                break;
            case monsterActionType.Move:    //이동상태, 고정된 방향으로만 이동
                this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
                anim.SetBool("move", true);
                break;
            case monsterActionType.Attack:  //공격 상태
                anim.SetBool("move", true);
                //플레이어와 자신의 위치에 따라 플립
                if (transform.position.x < player.transform.position.x)
                {
                    if (transform.rotation.eulerAngles.y > 100)
                        transform.Rotate(0, 180, 0);
                }
                else
                {
                    if (transform.rotation.eulerAngles.y < 100)
                        transform.Rotate(0, 180, 0);
                }

                
                
               //플레이어 레이어만 검출해내는 레이캐스트
                if (Physics2D.Raycast(transform.position,transform.right,attackRange,layerMask))
                {
                    //공격 실행중이 아닐때 공격 
                    if(!isAttacking)
                        StartCoroutine(DoAttack());
                }
                //공격 실행중이 아닐때 플레이어를 향해 이동
                if (!isAttacking)
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                break;
            case monsterActionType.Gethit: //방어 상태
                break;
            case monsterActionType.Die:     //죽음
                return;
                
        }
    }

    protected void movementChange()
    {
       
        int index = Random.Range(-1, 2);    //매번 랜덤한 행동 상태 부여

        if (index == 0) 
            monsterAction = monsterActionType.Idle;
        else
        {
            //이동방향에 맞게 플립
            if (index == 1)
            {
                
                if(transform.rotation.eulerAngles.y>100)
                    transform.Rotate(0, 180, 0);
            }
            else
            {
                
                
                if (transform.rotation.eulerAngles.y < 100)
                    transform.Rotate(0, 180, 0);
            }

            
            monsterAction = monsterActionType.Move;
        }
        

    }

    protected void detectPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 3f && monsterAction != monsterActionType.Attack)
            Attack();
        else if (Vector3.Distance(transform.position, player.transform.position) >= 3f && monsterAction == monsterActionType.Attack)
            CancelAttack();
    }

    public void Attack()    //공격 상태일때는 기존 실행중이던 코루틴을 종료하고 현재상태를 공격으로 바꿈
    {
        CancelInvoke();
        monsterAction = monsterActionType.Attack;
    }

    public void GiveDamage()    //애니메이션 이벤트로 호출
    {
        if (Physics2D.Raycast(transform.position, transform.right, attackRange, layerMask))
        {
            player.getDamage(attackDamage);
        }
    }

    protected IEnumerator DoAttack()
    {
        isAttacking = true;
        anim.SetTrigger("attack");  //공격 애니메이션 실행 후
        yield return new WaitForSeconds(attackSpeed);   //공격속도만큼 대기
        isAttacking = false;
    }

    public void GetAttacked(int damage)     //애니메이션 이벤트 호출에서 참조
    {
        anim.SetTrigger("getHit");
        mosnterHp -= damage;
        hpBar.fillAmount = (float)mosnterHp / maxHp;
        Debug.Log(mosnterHp);
    }
    


    public void CancelAttack()
    {
        monsterAction = monsterActionType.Idle; //플레이어가 일정 범위 밖으로 나갔다면 다시 기본상태로 회귀
        InvokeRepeating("movementChange", 2f, 2.3f);
    }

    protected void Dead()
    {
        if(mosnterHp<=0 && !isDead)
        {
            StopAllCoroutines();
            CancelInvoke();
            isDead = true;
            monsterAction = monsterActionType.Die;
            anim.SetBool("isDead",true);
            anim.SetTrigger("Die");

            Destroy(gameObject, 2f);
        }
    }
}
