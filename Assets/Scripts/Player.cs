using UnityEngine;

public class Player : Character
{
    Animator animator;
    public float speed = 5f;
    float moveX, moveY, lastMoveX = 1f, lastMoveY = 0f; // 마지막 입력 방향 (Idle 전환 시 유지)
    bool isMoving;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
    }
    void Update()
    {
        Move();
        SetAnimParams();
    }
    private void FixedUpdate()
    {

    }
    protected override void Death()
    {
        //플레이어 사망처리
    }

    protected override void Move()
    {
        //플레이어 움직임
        (moveX, moveY) = (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(new Vector2(moveX, moveY).normalized * speed * Time.deltaTime);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //회복처리
        //아이템, 경험치 처리
        //
    }
}
