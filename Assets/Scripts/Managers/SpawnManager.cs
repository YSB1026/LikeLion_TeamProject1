using Singleton.Component;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : SingletonComponent<SpawnManager>
{
    public GameObject monster;

    #region Singleton
    protected override void AwakeInstance()
    {
        PoolManager.Instance.CreatePool(monster, 10);
        StartCoroutine(MonsterSpawn(monster, 10));
    }

    protected override bool InitInstance()
    {
        return true;
    }

    protected override void ReleaseInstance()
    {
        
    }
    #endregion

    //몬스터 스폰 코루틴
    IEnumerator MonsterSpawn(GameObject prefab, int count, float delay = 1f, int amount = 1)
    {
        while(count > 0)
        {
            yield return new WaitForSeconds(delay);
            for(int i = 0; i < amount; i++)
            {
                GameObject mob = PoolManager.Instance.Get(prefab);
                mob.transform.position = GetRandomPosition();
            }
            count--;
        }
    }

    //무작위 스폰 위치를 반환하는 메서드
    private Vector2 GetRandomPosition()
    {
        Vector2 randomPosition = Vector2.zero;
        float min = -0.1f;
        float max = 1.1f;

        int flag = Random.Range(0, 4);
        switch (flag)
        {
            case 0:
                randomPosition = new Vector2(max, Random.Range(min, max));
                break;
            case 1:
                randomPosition = new Vector2(min, Random.Range(min, max));
                break;
            case 2:
                randomPosition = new Vector2(Random.Range(min, max), max);
                break;
            case 3:
                randomPosition = new Vector2(Random.Range(min, max), min);
                break;
        }
        randomPosition = Camera.main.ViewportToWorldPoint(randomPosition);
        Debug.Log(randomPosition);

        return randomPosition;
    }
}
