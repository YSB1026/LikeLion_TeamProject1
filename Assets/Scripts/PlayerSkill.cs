using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private Player player;
    public Skill attackPowerSkill;
    public Skill moveSpeedSkill;
    public Skill attackSpeedSkill;
    public Skill projectileSpeedSkill;
    public Skill maxHealthIncreaseSkill;

    private void OnValidate()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        // 스킬 초기화
        attackPowerSkill = new Skill("AttackPower", 3);//공격력
        moveSpeedSkill = new Skill("MoveSpeed", 3);//이동속도
        attackSpeedSkill = new Skill("AttackSpeed", 3);//공격속도
        projectileSpeedSkill = new Skill("ProjectileSpeed", 3);//투사체 속도
        maxHealthIncreaseSkill = new Skill("MaxHealthIncrease", 3);//최대체력 증가
    }

    public bool TryLevelUpSkill(Skill skill, int experience)
    {
        if (skill.CanLevelUp(experience))//레벨업이 가능하면
        {
            player.experience -= skill.RequiredExp();//경험치 차감
            skill.LevelUp();//레벨업
            ApplySkillEffect(skill);//효과 적용
            return true;//true 반환
        }
        return false;//레벨업이 안되면 false반환
    }

    private void ApplySkillEffect(Skill skill)
    {
        //일단 하드코딩으로 구현
        //리터럴은 초기에 설정해둔 값.
        switch (skill.SkillName)
        {
            case "AttackPower"://공격력
                player.atkPower = 2 + skill.Level;
                if (skill.hasPerk) player.knockbackPower += 2; // 특전: 넉백 증가
                break;

            case "MoveSpeed"://이동속도
                player.moveSpeed = 5f * (1f + skill.Level * 0.1f);
                if (skill.hasPerk) player.evasionChance = 30f; // 특전: 회피 확률 증가
                break;

            case "AttackSpeed"://공격속도
                player.atkSpeed = 1f * (1f - skill.Level * 0.1f);
                if (skill.hasPerk) player.projectileCount += 2; // 특전: 투사체 추가
                break;

            case "ProjectileSpeed"://투사체 속도
                player.projectileSpeed = 10f * (1f + skill.Level * 0.2f);
                if (skill.hasPerk) player.projectilePenetration += 2; // 특전: 투사체 관통 증가
                break;

            case "MaxHealthIncrease"://최대체력 증가
                player.maxHealth = 2 + skill.Level;
                if (skill.hasPerk) player.health = player.maxHealth; // 특전: 주변 데미지
                break;
        }
    }
}
