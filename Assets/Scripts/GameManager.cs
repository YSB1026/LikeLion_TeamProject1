using Singleton.Component;
using System;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    protected override void AwakeInstance()
    {
        Debug.Log("게임매니저 테스트");
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }
}
