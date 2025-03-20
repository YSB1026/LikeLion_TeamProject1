using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    //풀링할 프리팹
    private GameObject prefab;

    //비활성화된 오브젝트들을 보관하는 리스트
    private List<GameObject> pool;

    //풀링된 오브젝트들의 부모 트랜스폼
    private Transform parent;

    //생성자: 프리팹과 초기 크기를 받아 풀 초기화
    public ObjectPool(GameObject prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        pool = new List<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    //새로운 오브젝트를 생성해서 풀에 추가하는 private 메서드
    private void CreateNewObject()
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.SetActive(false);
        pool.Add(obj);
    }

    //풀에서 사용 가능한 오브젝트를 가져오는 메서드
    //풀이 비어있으면 새로 생성
    public GameObject Get()
    {
        Debug.Log(pool.Count);
        if (pool.Count == 0)
        {
            CreateNewObject();
        }

        GameObject obj = pool[0];
        obj.SetActive(true);
        pool.RemoveAt(0);
        return obj;
    }

    //사용이 끝난 오브젝트를 풀로 반환하는 메서드
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Add(obj);
    }
}
