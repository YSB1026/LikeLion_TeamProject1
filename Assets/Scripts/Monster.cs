using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : Character
{
    [Header("몬스터 속성")]
    public float speed = 1.5f;
    public GameObject[] expOrb;
    public float attackDistance = 5f;

    protected GameObject player;
    protected bool isDeath = false;
    protected bool isAttack = false;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        // 플레이어의 위치로 이동
        Vector3 direction = (player.transform.position - gameObject.transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected override void Death()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
