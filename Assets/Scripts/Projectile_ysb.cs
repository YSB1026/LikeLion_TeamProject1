using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2 dir;
    [SerializeField] private float speed;//player로부터 받아옴.
    [SerializeField] private int damage;//player로부터 받아옴.

    public void Initialize(Player owner, Vector2 dir)
    {
        transform.position = owner.transform.position;
        speed = owner.projectileSpeed;
        damage = owner.atkPower;
        this.dir = dir;
        if(this.dir == Vector2.zero)
            this.dir = Vector2.right;
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            //몬스터에게 데미지 주는 로직
            //Monster monster = collision.gameObject.GetComponent<Monster>();
            //Monster.TakeDamage(damage);

            PoolManager.Instance.Return(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        PoolManager.Instance.Return(gameObject);
    }

    private void Move()
    {
        rb.linearVelocity = dir.normalized * speed;
    }
}
