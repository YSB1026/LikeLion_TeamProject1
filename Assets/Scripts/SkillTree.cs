using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private string skillName;
    [SerializeField] private Button upgradeBtn;
    [SerializeField] private GameObject[] perks = new GameObject[3];
    [SerializeField] private string[] perkDescs = new string[3];

    private void OnValidate()
    {
        upgradeBtn = transform.GetChild(0).GetComponent<Button>();
        perks[0] = transform.GetChild(2).gameObject;
        perks[1] = transform.GetChild(3).gameObject;
        perks[2] = transform.GetChild(4).gameObject;
    }

    private void Awake()
    {
        SkillManager.Instance.Skills[index] = new Skill(skillName, 3);
        upgradeBtn.onClick.AddListener(Upgrade);
        perks[0].GetComponent<Image>().color = new Color32(40, 40, 40, 255);
        perks[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = perkDescs[0];
        perks[1].GetComponent<Image>().color = new Color32(40, 40, 40, 255);
        perks[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = perkDescs[1];
        perks[2].GetComponent<Image>().color = new Color32(40, 40, 40, 255);
        perks[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = perkDescs[2];
    }

    public void Upgrade()
    {
        if(SkillManager.Instance.TryLevelUpSkill(SkillManager.Instance.Skills[index]))
        {
            perks[SkillManager.Instance.Skills[index].Level - 1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            perks[SkillManager.Instance.Skills[index].Level - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
        }
    }
}
