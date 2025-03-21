using Singleton.Component;
using System.Collections;
using UnityEngine;

public class Launcher: MonoBehaviour
{
    private Player owner;
    private ProjectilePool projectilePool;
    private Coroutine fireCoroutine;
    private bool isApplicationQuit = false;

    private void Awake()
    {
        owner = GetComponentInParent<Player>();
        projectilePool = GetComponent<ProjectilePool>();
    }
    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        while (!isApplicationQuit)
        {
            projectilePool.SpawnProjectile();
            yield return new WaitForSeconds(owner.atkSpeed);
        }
    }

    /// <summary>
    /// 공격 속도가 바뀌면 호출해주세요 :)
    /// </summary>
    public void UpdateFireRate()
    {
        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);

        fireCoroutine = StartCoroutine(FireRoutine()); // 다시 시작
    }
    private void OnDestroy()
    {
        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);
    }

    private void OnApplicationQuit()
    {
        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);
    }
}
