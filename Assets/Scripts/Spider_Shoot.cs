using UnityEngine;

public class Spider_Shoot : Monster
{
    [Header("적 캐릭터 속성")]
    public float detectionRange = 3f;   //플레이어를 감지할 수있는 최대 거리
    public float shootingInterval = 2f;  //미사일 발사 사이의 대기 시간
    public GameObject missilePrefab;     //발사할 미사일 프리팹

    [Header("참조 컴포넌트")]
    public Transform firePoint;          //미사일이 발사될 위치
    private float shootTimer;           //발사 타이머
    
    void Start()
    {
        //필요한 컴포넌트 초기화
        shootTimer = shootingInterval; //타이머 초기화
 
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();  // SpriteRenderer가 null일 경우 초기화
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer가 존재하지 않습니다!", this);
            }
        }
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다!", this);
        }
    }
    void Update()
    {
        if (player == null) return;     //플레이어가 없으면 실행하지 않음
        base.Move();

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = (player.transform.position.x < transform.position.x);
        }
        //플레이어와의 거리 계산
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            //플레이어 방향으로 스프라이트 회전
            spriteRenderer.flipX = (player.transform.position.x > transform.position.x);


            //미사일 발사 로직
            shootTimer -= Time.deltaTime;   //타이머 감소

            if (shootTimer <= 0)
            {
                Shoot();            //미사일 발사
                shootTimer = shootingInterval; //타이머 리셋
            }

        }
    }
    //미사일 발사 함수
    void Shoot()
    {
        //미사일 생성
        GameObject missile = Instantiate(missilePrefab, firePoint.position, Quaternion.identity);

        missile.GetComponent<Spider_Missile>().SetDamage(atkPower);

        //플레이어 방향으로 발사 방향 설정
        Vector2 direction = (player.transform.position - firePoint.position).normalized;
        missile.GetComponent<Spider_Missile>().SetDirection(direction);
        missile.GetComponent<SpriteRenderer>().flipX = (player.transform.position.x < transform.position.x);
    }

    //디버깅용 기즈모
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}