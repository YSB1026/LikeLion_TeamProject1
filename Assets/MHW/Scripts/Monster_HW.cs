using System.Collections;
using UnityEngine;

public class Monster_HW : Character
{
    public GameObject player;
    public float speed = 1.5f;

    private SpriteRenderer spriteRenderer;
    Animator animator;


    protected override void Death()
    {
        animator.SetBool("isDeath", true);
        Destroy(gameObject, 0.7f);
    }

    protected override void Move()
    {
        if (player != null)
        {
            // 플레이어의 위치로 이동
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // 플레이어의 위치에 따라 스프라이트 방향 설정, 몬스터가 왼쪽에 있으면 flip
            spriteRenderer.flipX = transform.position.x < player.transform.position.x;
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        
    }

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            Death();
        }
    }
}
