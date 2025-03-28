using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    /*
    Character.cs에서 상속받음
    public float moveSpeed = 5f; //이동 속도
    public int health = 2; //체력
    public int atkPower = 2; //공격력
    public float atkSpeed = 1f; //공격 속도
    */
    [Header("플레이어 속성")]
    public int maxHealth;//플레이어 최대 체력
    public float evasionChance = 0; //플레이어 회피 확률
    public float projectileSpeed = 10f; //플레이어 투사체 속도
    public float knockbackPower = 1f; //플레이어 투사체 넉백
    public int projectilePenetration = 1; //플레이어 투사체 관통력
    public int projectileCount = 1; //플레이어 투사체 개수
    public int experience = 0; //플레이어 경험치

    private bool isAlive = true; //플레이어 생존 여부
    [SerializeField] private PlayerSkill playerSkill;
    private Animator animator;
    private float moveX, moveY;

    [Header("참조")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject auraEffect;
    [SerializeField] private Slider healthBar;


    private void Awake()
    {
        maxHealth = health; //최대체력 초기화
        animator = GetComponent<Animator>();
        PoolManager.Instance.CreatePool(projectilePrefab, 10);
        StartCoroutine(FireProjectile());
        auraEffect.SetActive(false);
    }

    private void Start()
    {
        SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        KeyInput();
        if (CanAct())
        {
            Move();//움직임 처리
            SetAnimParams();//애니메이터 파라미터 설정
            ApplyMaxHealthPerkDamage();
        }
        HandleSkillLevelUp();//스킬 레벨업 처리
    }

    public void SetMaxHealth(int maxHp)
    {
        int diff = maxHp - maxHealth;
        maxHealth = maxHp;
        healthBar.maxValue = maxHealth;
        if(diff > 0)
        {
            health += diff;
        }
        else if(health > maxHealth)
        {
            health = maxHealth;
        }
        SetHealth(health);
    }

    private void SetHealth(int hp)
    {
        healthBar.value = hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //회복처리
    }

    public override void TakeDamage(int damage)
    {
        if (playerSkill.moveSpeedSkill.hasPerk && Random.value * 100 < evasionChance)//이동속도 특전이 있는경우 30퍼센트(임시) 확률로 회피
        {
            Debug.Log("회피 성공");
            //뭔가 이펙트가 있어야할지도..
            return;
        }

        base.TakeDamage(damage);
        SetHealth(health);
        StartCoroutine(TakeDamageRoutine());
    }

    protected override void Death()
    {
        //플레이어 사망처리
        isAlive = false;
        StopAllCoroutines();
        auraEffect.SetActive(false);
    }

    protected override void Move()
    {
        //플레이어 움직임
        (moveX, moveY) = (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(new Vector2(moveX, moveY).normalized * moveSpeed * Time.deltaTime);
    }

    private void SetAnimParams()
    {
        Vector3 direction = (MouseManager.Instance.transform.position - transform.position).normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) // 좌우
        {
            animator.SetFloat("dirX", direction.x > 0 ? 1f : -1f);
            animator.SetFloat("dirY", 0f);
        }
        else //상하
        {
            animator.SetFloat("dirY", direction.y > 0 ? 1f : -1f);
            animator.SetFloat("dirX", 0f);
        }
    }

    IEnumerator FireProjectile()
    {
        while (true)
        {
            yield return new WaitForSeconds(atkSpeed);
            //PlayerProjectile projectile = PoolManager.Instance.Get(projectilePrefab).GetComponent<PlayerProjectile>();
            Vector3 baseDirection = (MouseManager.Instance.mousePos - transform.position).normalized;
            if (playerSkill.attackSpeedSkill.hasPerk)//공격 속도 특전, 투사체 개수 증가(좌우로 하나씩 추가)
            {
                float angleOffset = 5f; // 좌우 발사 각도 차이
                for (int i = -1; i <= 1; i++)
                {
                    float angle = i * angleOffset;
                    Vector3 rotatedDirection = Quaternion.Euler(0, 0, angle) * baseDirection;
                    FireProjectile(rotatedDirection);
                }
            }
            else
            {
                FireProjectile(baseDirection);
            }
        }
    }
    void FireProjectile(Vector3 direction)
    {
        PlayerProjectile projectile = PoolManager.Instance.Get(projectilePrefab).GetComponent<PlayerProjectile>();
        projectile.Initialize(this);
        projectile.transform.position = transform.position;
        projectile.direction = direction;
    }

    IEnumerator TakeDamageRoutine()
    {
        //이후 구현
        yield return null;
    }

    private void HandleSkillLevelUp()
    {
        Skill skill = null;
        //추후에 UI로 변경
        if (Input.GetKeyDown(KeyCode.Alpha1)) skill = playerSkill.attackPowerSkill;//공격력
        if (Input.GetKeyDown(KeyCode.Alpha2)) skill = playerSkill.moveSpeedSkill;//이동속도
        if (Input.GetKeyDown(KeyCode.Alpha3)) skill = playerSkill.attackSpeedSkill;//공격속도
        if (Input.GetKeyDown(KeyCode.Alpha4)) skill = playerSkill.projectileSpeedSkill;//투사체 속도
        if (Input.GetKeyDown(KeyCode.Alpha5)) skill = playerSkill.maxHealthIncreaseSkill;//최대체력 증가

        if (skill != null)
        {
            playerSkill.TryLevelUpSkill(skill, experience);
        }
    }

    private void ApplyMaxHealthPerkDamage()
    {
        if (playerSkill.maxHealthIncreaseSkill.hasPerk && !auraEffect.activeSelf) // 최대 체력 특전 && auraEffect가 활성화 돼있지 않을 때
        {
            auraEffect.SetActive(true);
            auraEffect.GetComponent<PlayerAura>().SetDamage(maxHealth);//최대 체력만큼 데미지
        }
    }

    private void KeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!UIManager.Instance.IsEscapeMenuOpen && !UIManager.Instance.IsMenuOpen)
            {
                UIManager.Instance.ToggleEscapeMenu();
            }
            else if (UIManager.Instance.IsMenuOpen)
            {
                UIManager.Instance.ToggleMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(CanAct() || UIManager.Instance.IsSkillTreeOpen)
            {
                UIManager.Instance.ToggleSkillTree();
            }
        }
    }

    private bool CanAct()
    {
        if(GameManager.Instance.IsGamePaused || !isAlive)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
