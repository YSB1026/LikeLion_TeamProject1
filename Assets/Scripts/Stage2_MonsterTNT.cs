using System.Collections;
using UnityEngine;

public class Stage2_MonsterTNT : Monster
{
    /*
    [Header("몬스터 기본 속성")]
    public float moveSpeed = 5f; //이동 속도
    public int health = 2; //체력
    public int atkPower = 2; //공격력
    public float atkSpeed = 1f; //공격 속도
    [Header("몬스터 속성")]
    public float speed = 1.5f;
    public GameObject[] expOrb;
    public float attackDistance = 5f;

    protected GameObject player;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    */
    public GameObject dynamite;
    private float distance;//플레이어와의 거리
    private Coroutine attackRoutine = null;
    private Vector2 attackDirection;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        attackDistance = 12;
    }
    void Update()
    {
        CalcPlayerDistance();
        Move();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
    protected override void Move()
    {
        if(distance > attackDistance)//사거리까지 player chase함.
        {
            StopAttackRoutnie();
            animator.SetBool("isMove", true);
            base.Move();
        }
        else//사거리 안인경우.
        {
            animator.SetBool("isMove", false);
            if (attackRoutine == null) attackRoutine = StartCoroutine(Attack());
        }
    }
    protected override void Death()
    {
        StopAttackRoutnie();
        PoolManager.Instance.Return(gameObject);
    }

    private void CalcPlayerDistance()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(atkSpeed);
            animator.SetTrigger("attack");
            attackDirection = (player.transform.position - transform.position).normalized;
            GameObject go = Instantiate(dynamite, transform.position, Quaternion.identity);
            go.GetComponent<Dynamite>().Initiate(
                distance : distance,
                damage : atkPower,
                direction : attackDirection
            );
        }
    }

    void StopAttackRoutnie()
    {
        if (attackRoutine == null) return;
        StopCoroutine(attackRoutine);
        attackRoutine = null;
    }
}
