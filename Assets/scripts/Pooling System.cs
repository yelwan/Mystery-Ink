using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    List<GameObject> activeProjectiles;
    Queue<GameObject> projectilePool;
    [SerializeField] Transform projectileParent;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] SprayParticles sprayParticles;
    [SerializeField] int NumOfPooledObjects = 10;



    void Start()
    {
        activeProjectiles = new List<GameObject>();
        projectilePool = new Queue<GameObject>();
        for (int i = 0; i < NumOfPooledObjects; i++)
        {
            CreatePooledProjectile(Vector2.zero);
        }
    }
    public void CreatedProjectilePublic(Vector2 hit)
    {
        CreateProjectile(hit);
    }
    void CreatePooledProjectile(Vector2 hit)
    {
        GameObject projectile = Instantiate(projectilePrefab, hit, Quaternion.identity);

        projectilePool.Enqueue(projectile);
        projectile.SetActive(false);
        projectile.transform.SetParent(projectileParent);
    }

     GameObject CreateProjectile(Vector2 hit)
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

        if (activeProjectiles.Count > NumOfPooledObjects)
        {
            ClearPool();
        }

        return projectile;
    }

     void ReturnToPool(GameObject _projectile)
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
