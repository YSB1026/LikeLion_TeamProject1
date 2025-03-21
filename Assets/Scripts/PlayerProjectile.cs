using UnityEngine;

public class PlayerProjectile : Projectile
{
    public Vector3 direction;

    protected override void Move()
    {
        rb.linearVelocity = direction * projectileSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().TakeDamage(damage);
            PoolManager.Instance.Return(gameObject);
        }
    }
}
