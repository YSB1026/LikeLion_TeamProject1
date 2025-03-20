using Unity.Cinemachine;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Camera cam;
    [SerializeField] float threshold;//시네머신 카메라가 플레이어 따라갈 때 threshold를 줘서 뭔가 하는거가 같음.
    public static MouseManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(cam == null)
        {
            Debug.LogError("메인 카메라를 찾을 수 없습니다.");
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
        Debug.Log(mousePos);
        Vector3 targetPos = (player.position + mousePos);

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + player.position.x, threshold+player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + player.position.y, threshold + player.position.y);

        this.transform.position = new Vector3(targetPos.x, targetPos.y, 0);
    }
    public Quaternion GetRotationInfo()
    {
        //Debug.Log(Quaternion.AngleAxis(angle - 90, Vector3.forward));
        return Quaternion.AngleAxis(90, Vector3.forward);
    }
}
