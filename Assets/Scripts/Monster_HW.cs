using System.Collections;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Monster_HW : Character
{
    public GameObject player;
    public float speed = 1.5f;
    public GameObject[] expOrb;

    private bool isDeath = false;

    private SpriteRenderer spriteRenderer;
    Animator animator;
    
    protected override void Death()
    {
        animator.SetBool("isDeath", true);
        isDeath = true;
        StartCoroutine(ReturnToPoolAfterDelay(0.7f)); // 0.7초후 풀반환

        CreateExpOrb();

    }
    protected override void Move()
    {
        if (isDeath == false) // 몬스터가 살아있을 때만
        {
            // 플레이어의 위치로 이동
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // 플레이어의 위치에 따라 스프라이트 방향 설정, 몬스터가 왼쪽에 있으면 flip
            spriteRenderer.flipX = transform.position.x < player.transform.position.x;
        }
    }

    // delay 시간 이후 오브젝트 풀에 리턴하는 코루틴
    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PoolManager.Instance.Return(gameObject);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();    
    }
    
    // 충돌처리, 추후 플레이어 공격
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Death();
        }
    }

    void CreateExpOrb()
    {
        GameObject expOrbInstance;
        if (gameObject.CompareTag("Monster1"))
        {
            expOrbInstance = Instantiate(expOrb[0], gameObject.transform.position, Quaternion.identity);
            Destroy(expOrbInstance, 5f);
        }
        else if (gameObject.CompareTag("Monster2"))
        {
            expOrbInstance = Instantiate(expOrb[1], gameObject.transform.position, Quaternion.identity);
            Destroy(expOrbInstance, 5f);
        }
        else
        {
            expOrbInstance = Instantiate(expOrb[2], gameObject.transform.position, Quaternion.identity);
            Destroy(expOrbInstance, 5f);
        }

    }

}
