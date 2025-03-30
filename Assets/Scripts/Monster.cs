using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
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
    public bool isDeath = false;
    protected bool isAttack = false;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    private int maxHealth;
    public void Initiate()
    {
        isDeath = false;
        health = maxHealth;
        Debug.Log($"{name} health: {health}");
    }

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Awake()
    {
        maxHealth = health;
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
    public override void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && !isDeath)
        {
            Death();
        }
    }
    protected override void Death()
    {
        isDeath = true;
        GameManager.Instance.KillScore++;
        PoolManager.Instance.Return(gameObject);
        CreateExpOrb();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isDeath && !isAttack && collision.CompareTag("Player"))
        {
            StartCoroutine(AttackRoutine(collision.GetComponent<Player>()));
        }
    }

    IEnumerator AttackRoutine(Player player)
    {
        isAttack = true;
        player.TakeDamage(atkPower);
        yield return new WaitForSeconds(2.5f);
        isAttack = false;
    }

    //수정 해야 함,
    //배열 대신 expOrb로만 구현 하거나, 배열로 구현하고 싶으면,
    //확률적으로 expOrb 0~2를 생성하고싶으면 Random함수로 구현
    //protected void CreateExpOrb()
    //{
    //    if (expOrb.Length == 0) return; // expOrb 배열이 비어 있으면 실행하지 않음

    //    int randomIndex = Random.Range(0, expOrb.Length); // 0 ~ 배열 길이-1 사이에서 랜덤 선택

    //    GameObject expOrbInstance = Instantiate(expOrb[randomIndex], gameObject.transform.position, Quaternion.identity);

    //    Destroy(expOrbInstance, 10f); // 10초 후 제거
    //}

    // 확률 설정한 랜덤 경험치
    protected void CreateExpOrb()
    {
        if (expOrb.Length == 0) return; // expOrb 배열이 비어 있으면 실행하지 않음

        // 0.0 ~ 1.0 사이의 랜덤 값 생성
        float randomValue = Random.value;

        // 확률에 따라 인덱스 선택
        int selectedIndex;
        if (randomValue < 0.6f)
        {
            selectedIndex = 0; // 60% 확률
        }
        else if (randomValue < 0.9f) // 0.6 + 0.3
        {
            selectedIndex = 1; // 30% 확률
        }
        else
        {
            selectedIndex = 2; // 10% 확률
        }

        // 선택된 인덱스로 오브젝트 생성
        GameObject expOrbInstance = Instantiate(expOrb[selectedIndex], gameObject.transform.position, Quaternion.identity);
        Destroy(expOrbInstance, 10f); // 10초 후 제거
    }
}
