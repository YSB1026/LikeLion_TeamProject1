using System.Collections;
using UnityEngine;

public class Player : Character
{
    /* Character.cs에서 상속받음
public float moveSpeed = 5f; //이동 속도
public int health = 2; //체력
public int atkPower = 2; //공격력
public float atkSpeed = 1f; //공격 속도
*/
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePos;
    public float projectileSpeed = 10f;

    private Animator animator;
    private float moveX, moveY, lastMoveX = 1f, lastMoveY = 0f; // 마지막 입력 방향 (Idle 전환 시 유지)
    private bool isMoving;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PoolManager.Instance.CreatePool(projectilePrefab, 10);
        StartCoroutine(FireProjectile());
    }
    void Start()
    {
    }
    protected override void Update()
    {
        base.Update();
        SetAnimParams();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //회복처리
        //아이템, 경험치 처리
        //
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(TakeDamageRoutine());
    }
    protected override void Death()
    {
        //플레이어 사망처리
    }

    protected override void Move()
    {
        //플레이어 움직임
        (moveX, moveY) = (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(new Vector2(moveX, moveY).normalized * moveSpeed * Time.deltaTime);
    }

    private void SetAnimParams()
    {
        isMoving = moveX != 0 || moveY != 0;
        if (isMoving)//키를 땠을때 어느 방향을 보고있는지 저장하기 위한 로직
        {
            if (Mathf.Abs(moveX) > Mathf.Abs(moveY)) moveY = 0;
            else moveX = 0;
            lastMoveX = moveX;
            lastMoveY = moveY;
        }

        animator.SetFloat("dirX", isMoving ? moveX : lastMoveX);
        animator.SetFloat("dirY", isMoving ? moveY : lastMoveY);
        animator.SetBool("isWalking", isMoving);
    }

    IEnumerator FireProjectile()
    {
        while (true)
        {
            yield return new WaitForSeconds(atkSpeed);
            GameObject projectile = PoolManager.Instance.Get(projectilePrefab);
            projectile.transform.position = firePos.position;
            projectile.GetComponent<PlayerProjectile>().direction = (MouseManager.Instance.mousePos - firePos.position).normalized;
        }
    }

    IEnumerator TakeDamageRoutine()
    {
        yield return null;
    }
}
