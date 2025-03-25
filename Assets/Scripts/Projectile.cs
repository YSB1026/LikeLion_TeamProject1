using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed = 8f; //투사체 속도
    [SerializeField] protected int damage = 1; //투사체 피해

    protected Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //투사체 이동 메서드
    protected abstract void Move();

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected void OnBecameInvisible()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
