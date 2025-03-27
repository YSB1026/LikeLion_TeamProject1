using Singleton.Component;
using System;
using UnityEngine;

public class GameManager : SingletonComponent<GameManager>
{
    [SerializeField] private bool isGamePaused = false;

    public bool IsGamePaused { get => isGamePaused; }

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

    public void PauseGame(bool pauseBool)
    {
        isGamePaused = pauseBool;
        Time.timeScale = isGamePaused ? 0f : 1f;
    }
}
