using Unity.Cinemachine;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Camera cam;
    [SerializeField] float threshold;//threshold -> clamp(min,max) 줄 값.
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

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
        Vector3 target = -mousePos; //나중에 알아보기 왜 반대로 되는지
        target.x = Mathf.Clamp(target.x, -threshold + player.position.x, threshold + player.position.x);
        target.y = Mathf.Clamp(target.y, -threshold + player.position.y, threshold + player.position.y);

        transform.position = new Vector3(target.x, target.y, 0);
    }
    public Quaternion GetRotationInfo()
    {
        //Debug.Log(Quaternion.AngleAxis(angle - 90, Vector3.forward));
        return Quaternion.AngleAxis(90, Vector3.forward);
    }
}
