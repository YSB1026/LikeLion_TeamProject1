using Singleton.Component;
using System.Collections;
using System.Threading;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public int count = 15;
    private Player owner;
    public void Awake()
    {
        owner = GetComponentInParent<Player>();
        PoolManager.Instance.CreatePool(projectilePrefab, count);
    }
    public void SpawnProjectile()
    {
        Projectile projectile = PoolManager.Instance.Get(projectilePrefab)
            .GetComponent<Projectile>();

        projectile.Initialize(owner,
            (MouseManager.Instance.transform.position - owner.transform.position).normalized);
    }
}
