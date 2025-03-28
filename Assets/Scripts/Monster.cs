using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : Character
{
    /* 수정해야함.
    public float moveSpeed = 5f; //이동 속도
    public int health = 2; //체력
    public int atkPower = 2; //공격력
    public float atkSpeed = 1f; //공격 속도
    */
    [Header("몬스터 속성")]
    public GameObject[] expOrb;
    public float attackDistance = 5f;

    protected GameObject player;
    protected bool isDeath = false;
    protected bool isAttack = false;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    private void OnEnable()
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
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    protected override void Death()
    {
        GameManager.Instance.KillScore++;
        PoolManager.Instance.Return(gameObject);
    }
}
