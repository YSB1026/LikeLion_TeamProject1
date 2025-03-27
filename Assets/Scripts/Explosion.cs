using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage(damage);
        }
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public void DestroyExplosion()//애니메이션 이벤트로 호출
    {
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
