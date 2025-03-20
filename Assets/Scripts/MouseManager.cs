using Singleton.Component;
using Unity.Cinemachine;
using UnityEngine;

public class MouseManager : SingletonComponent<MouseManager>
{
    [SerializeField] Transform player;
    [SerializeField] float threshold;//threshold -> clamp(min,max) 줄 값.

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

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        //Vector3 target = -mousePos; //나중에 알아보기 왜 반대로 되는지, 느낌상 z값이 음수라서 그런거같음.
        //target.x = Mathf.Clamp(target.x, -threshold + player.position.x, threshold + player.position.x);
        //target.y = Mathf.Clamp(target.y, -threshold + player.position.y, threshold + player.position.y);
        //mousePos = -mousePos;
        transform.position = mousePos;
    }
    public Quaternion GetRotationInfo()
    {
        //Debug.Log(Quaternion.AngleAxis(angle - 90, Vector3.forward));
        return Quaternion.AngleAxis(90, Vector3.forward);
    }
}
