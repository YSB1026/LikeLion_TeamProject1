using Singleton.Component;
using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonComponent<GameManager>
{
    [SerializeField] private bool isGamePaused = false;
    [SerializeField] private int experience = 0; //플레이어 경험치
    [SerializeField] private int killScore = 0; //몬스터 킬 수

    [SerializeField] private GameObject portalPrefab; // 포털 프리팹
    
    private GameObject currentPortal; // 현재 포털
    private bool isStageCleared = false;

    private int currentStage = 0; // 현재 스테이지
    private int maxStage = 5; // 총 스테이지 수

    public Vector2 portalSpawnPosition; // 포털 생성 위치

    public bool IsGamePaused { get => isGamePaused; }
    public int Experience { get => experience; }
    public int KillScore { get => killScore; set => killScore = value; }

    #region Singleton
    protected override void AwakeInstance()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CreatePortalForCurrentStage();
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }
    #endregion

    public void SetPause(bool pauseBool)
    {
        isGamePaused = pauseBool;
        Time.timeScale = isGamePaused ? 0f : 1f;
    }

    public void UpdateExp(int amount)
    {
        experience += amount;
        UIManager.Instance.SetExp();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CreatePortalForCurrentStage();
    }

    public void CreatePortalForCurrentStage()
    {
        if (currentStage <= maxStage)
        {
            // 포털 프리팹을 현재 스테이지 위치에 생성
            Instantiate(portalPrefab, portalSpawnPosition, Quaternion.identity);
            Debug.Log($"Stage {currentStage}에 포털이 생성되었습니다.");
        }
    }

    public void LoadNextStage()
    {
        if (currentStage < maxStage)
        {
            currentStage++;
            string nextScene = "Stage" + currentStage;
            Debug.Log($"다음 스테이지로 이동: {nextScene}");
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.Log("모든 스테이지 클리어!");
        }
    }
}
