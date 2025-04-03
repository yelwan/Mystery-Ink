using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    public static PoolingSystem instance;
    List<GameObject> activeProjectiles;
    Queue<GameObject> projectilePool;
    [SerializeField] Transform projectileParent;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] SprayParticles sprayParticles; 

    private void Awake() => instance = this;

    void Start()
    {
        activeProjectiles = new List<GameObject>();
        projectilePool = new Queue<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            CreatePooledProjectile(Vector2.zero);
        }
    }

    void CreatePooledProjectile(Vector2 hit)
    {
        GameObject projectile = Instantiate(projectilePrefab, hit, Quaternion.identity);

        projectilePool.Enqueue(projectile);
        projectile.SetActive(false);
        projectile.transform.SetParent(projectileParent);
    }

    public GameObject CreateProjectile(Vector2 hit)
    {
        GameObject projectile;

        if (projectilePool.Count == 0)
        {
            projectile = Instantiate(projectilePrefab, hit, Quaternion.identity);
        }
        else
        {
            projectile = projectilePool.Dequeue();
        }

        activeProjectiles.Add(projectile);
        projectile.transform.position = hit; 
        projectile.SetActive(true);

        if (activeProjectiles.Count > 10)
        {
            ClearPool();
        }

        return projectile;
    }

    public void ReturnToPool(GameObject _projectile)
    {
        if (!activeProjectiles.Remove(_projectile)) return;

        _projectile.SetActive(false);
        projectilePool.Enqueue(_projectile);
    }

    void ClearPool()
    {
        List<GameObject> projectilesToClear = new List<GameObject>(activeProjectiles);
        foreach (GameObject projectile in projectilesToClear)
        {
            ReturnToPool(projectile);
        }
    }
}
