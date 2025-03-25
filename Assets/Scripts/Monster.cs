using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : Character
{
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        
    }

    protected override void Death()
    {
        PoolManager.Instance.Return(gameObject);
    }
}
