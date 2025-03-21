using Singleton.Component;
using UnityEngine;


public class MouseManager : SingletonComponent<MouseManager>
{
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
        transform.position = mousePos;
    }
}