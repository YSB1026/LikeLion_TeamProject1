using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalManager : MonoBehaviour
{
    public static PortalManager Instance;

    public GameObject portalPrefab; // 포털 프리팹
    private GameObject currentPortal; // 현재 포털
    private bool isStageCleared = false;

    private int currentStage = 0; // 현재 스테이지
    private int maxStage = 5; // 총 스테이지 수

    public Vector2 portalSpawnPosition; // 포털 생성 위치

    //테스트 코드(게임 시작 시 포털을 생성하고 다음 씬으로 넘어가도 자동으로 포털이 생성됨)
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CreatePortalForCurrentStage();
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
    //여기까지 테스트

    //PortalManager가 하나만 유지되도록 함.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 변경 후에도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //스테이지 클리어메서드(수정 필요)
    /*public void ClearStage()
    {
        if (!isStageCleared)
        {
            isStageCleared = true;
            Debug.Log("스테이지 클리어! 포털이 생성됩니다.");
            CreatePortal(); // 스테이지 클리어 시 포털 생성
        }
    }*/

    /*private void CreatePortal()
    {
        if (currentPortal == null) // 중복 생성 방지
        {
            currentPortal = Instantiate(portalPrefab, portalSpawnPosition, Quaternion.identity);
            Debug.Log("포털 생성 완료! 위치: " + portalSpawnPosition);
        }
    }*/

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
