using Singleton.Component;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : SingletonComponent<UIManager>
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private bool isMenuOpen = false;

    [Header("Skill UI")]
    [SerializeField] private bool isSkillTreeOpen = false;
    [SerializeField] private GameObject skillTreePanel;

    [Header("Escape Menu UI")]
    [SerializeField] private bool isEscapeMenuOpen = false;
    [SerializeField] private GameObject escapeMenuPanel;

    public bool IsMenuOpen { get => isMenuOpen; }
    public bool IsSkillTreeOpen { get => isSkillTreeOpen; }
    public bool IsEscapeMenuOpen { get => isEscapeMenuOpen; }

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

    public void ToggleMenu()
    {
        if (isMenuOpen)
        {
            isMenuOpen = !isMenuOpen;

            switch (true)
            {
                case bool _ when isSkillTreeOpen:
                    ToggleSkillTree();
                    break;
                case bool _ when isEscapeMenuOpen:
                    ToggleEscapeMenu();
                    break;
                default:
                    break;
            }
        }
    }

    public void ToggleSkillTree()
    {
        isSkillTreeOpen = !isSkillTreeOpen;
        SetBooleans(skillTreePanel, isSkillTreeOpen);
    }

    public void ToggleEscapeMenu()
    {
        isEscapeMenuOpen = !isEscapeMenuOpen;
        SetBooleans(escapeMenuPanel, isEscapeMenuOpen);
    }

    private void SetBooleans(GameObject menu, bool menuBool)
    {
        isMenuOpen = menuBool;
        menu.GetComponent<Canvas>().enabled = menuBool;
        GameManager.Instance.PauseGame(menuBool);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
