using UnityEngine;

public class Player : Character
{
    float moveX, moveY, lastMoveX = 1f, lastMoveY = 0f;
    bool isMoving;

    Animator pAnimator;
    Rigidbody2D pRig2D;
    SpriteRenderer sp;
    Collider2D collider;

    private void Awake()
    {
        pRig2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        pAnimator = GetComponent<Animator>();
    }

    protected override void Death()
    {

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

        pAnimator.SetFloat("dirX", isMoving ? moveX : lastMoveX);
        pAnimator.SetFloat("dirY", isMoving ? moveY : lastMoveY);
        pAnimator.SetBool("isWalking", isMoving);
    }

    void Update()
    {
        Move();
        SetAnimParams();
    }
}
