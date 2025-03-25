using UnityEngine;

public class PlayerProjectile : Projectile
{
    private Vector3 direction;

    void Update()
    {
        Move();
    }

    public void SetProjectileStat(Vector3 pos, Vector3 dir, float _projectileSpeed, int _damage)
    {
        transform.position = pos;
        direction = dir;
        projectileSpeed = _projectileSpeed;
        damage = _damage;
    }

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
