using Singleton.Component;
using System;
using System.Collections;
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

    [SerializeField] private int currentStage = 0; // 현재 스테이지
    private int maxStage = 5; // 총 스테이지 수

    public Vector2 portalSpawnPosition; // 포털 생성 위치

    bool isLoading = false;

    public bool IsGamePaused { get => isGamePaused; }
    public int Experience { get => experience; }
    public int KillScore { get => killScore; set => killScore = value; }

    #region Singleton
    protected override void AwakeInstance()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

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

    public void UpdatekillScore(int score)
    {
        killScore += score;
        UIManager.Instance.ToggleDeathMessage();
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
        //if (isLoading) return;  // 이미 로드 중이면 실행하지 않음
        //isLoading = true;        // 플래그 설정

        //if (currentStage < maxStage)
        //{
        //    currentStage++;
        //    string nextScene = "Stage" + currentStage;
        //    Debug.Log($"다음 스테이지로 이동: {nextScene}");
        //    SceneManager.LoadScene(nextScene);
        //}
        //else
        //{
        //    Debug.Log("모든 스테이지 클리어!");
        //}

        //isLoading = false;

        if (!isLoading)
        {
            StartCoroutine(LoadSceneRoutine());

        }

    }

    private IEnumerator LoadSceneRoutine()
    {
        isLoading = true;        // 플래그 설정

        if (currentStage < maxStage)
        {
            currentStage++;
            string nextScene = "Stage" + currentStage;
            Debug.Log($"다음 스테이지로 이동: {nextScene}");
            SceneManager.LoadScene(nextScene);
            BgmController(currentStage);
        }
        else
        {
            Debug.Log("모든 스테이지 클리어!");
        }

        isLoading = false;
        yield return new WaitForSeconds(1f);
    }
    private void BgmController(int stage)
    {
        switch (stage)
        {
            case 1:
                AudioManager.instance.PlayBgm(AudioManager.Bgm.Stage1);
                break;
            case 2:
                AudioManager.instance.PlayBgm(AudioManager.Bgm.Stage2);
                break;
            case 3:
                AudioManager.instance.PlayBgm(AudioManager.Bgm.Stage3);
                break;
            case 4:
                AudioManager.instance.PlayBgm(AudioManager.Bgm.Stage4);
                break;
            case 5:
                AudioManager.instance.PlayBgm(AudioManager.Bgm.Stage5);
                break;
            default:
                Debug.LogWarning($"알 수 없는 스테이지: {stage}. BGM 재생 안 함");
                break;
        }
    }
}
