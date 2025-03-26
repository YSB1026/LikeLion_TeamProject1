using Singleton.Component;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : SingletonComponent<UIManager>
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private bool isMenuOpen = false;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private bool isSkillTreeOpen = false;
    [SerializeField] private GameObject skillTreePanel;

    public bool IsMenuOpen { get => isMenuOpen; }
    public bool IsSkillTreeOpen { get => isSkillTreeOpen; }

    #region Singleton
    protected override void AwakeInstance()
    {
        
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
