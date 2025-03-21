using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float projectileSpeed = 8f; //투사체 속도
    public int damage = 1; //투사체 피해

    //투사체 이동 메서드
    protected abstract void Move();

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    private void OnBecameInvisible()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
