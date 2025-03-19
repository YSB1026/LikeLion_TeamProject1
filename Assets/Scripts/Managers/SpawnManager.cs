using Singleton.Component;
using UnityEngine;

public class SpawnManager : SingletonComponent<SpawnManager>
{
    public GameObject monster;

    protected override void AwakeInstance()
    {
        PoolManager.Instance.CreatePool(monster, 10);
        for(int i = 0; i < 15; i++)
        {
            PoolManager.Instance.Get(monster);
        }
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }
}
