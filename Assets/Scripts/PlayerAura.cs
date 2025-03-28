using UnityEngine;

public class PlayerAura : MonoBehaviour
{
    [SerializeField] private int damage;
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<Monster>().TakeDamage(damage);
        }
    }
}
