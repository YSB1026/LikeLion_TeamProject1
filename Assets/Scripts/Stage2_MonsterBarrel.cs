using Unity.VisualScripting;
using UnityEngine;

public class Stage2_MonsterBarrel : Monster
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
    */

    [Header("폭발 설정")]
    public float explosionRadius = 2.5f; // 폭발 반경
    public float detonationDistance = 2.0f; // 플레이어와 이 거리 내에 들어오면 폭발

    private bool hasExploded = false;
    private float distanceToPlayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detonationDistance && !hasExploded)
        {
            Explode();
        }
        else if(!hasExploded)
        {
            base.Move();
            animator.SetBool("isMove", true);
        }
    }

    private void Explode()
    {
        hasExploded = true;
        animator.SetBool("hasExploded", hasExploded);
        animator.SetBool("isMove", false);
    }

    public void DestroyBarrel()//애니메이션 이벤트로 호출
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D obj in hitObjects)
        {
            if (obj.CompareTag("Player"))
            {
                obj.GetComponent<Player>().TakeDamage(atkPower);
            }
        }
        PoolManager.Instance.Return(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        // 폭발 범위 시각화 (디버깅 용도)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    protected override void Death()
    {
        base.Death();
    }
}
