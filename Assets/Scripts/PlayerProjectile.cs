using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProjectile : Projectile
{
    /*Projectile에서 상속받음
    public float projectileSpeed = 8f; //투사체 속도
    public int damage = 1; //투사체 피해
    */
    public float knockbackPower;//투사체 넉백 계수
    public int pentration; //관통력
    public Vector3 direction;

    public void Initialize(Player player)
    {
        damage = player.atkPower;
        knockbackPower = player.knockbackPower;
        pentration = player.projectilePenetration;
    }
    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        rb.linearVelocity = direction * projectileSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().TakeDamage(damage);
            //넉백 처리 해야함.
            pentration--;
            if (pentration <= 0)
            {
                PoolManager.Instance.Return(gameObject);  // 충돌 시 풀로 반환
            }
        }
    }
}
