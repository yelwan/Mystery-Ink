using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    [SerializeField] Transform projectileParent;

    [Header("Splat Prefabs")]
    [SerializeField] GameObject RedSplat_1;
    [SerializeField] GameObject RedSplat_2;
    [SerializeField] GameObject BlueSplat_1;
    [SerializeField] GameObject BlueSplat_2;
    [SerializeField] GameObject YellowSplat_1;
    [SerializeField] GameObject YellowSplat_2;

    [SerializeField] int NumOfPooledObjects = 2500;

    [SerializeField] SprayParticles sprayParticles;
    [SerializeField] InventorySystem inventorySystem;

    private Dictionary<string, Queue<GameObject>> splatPools = new();
    private Dictionary<string, GameObject[]> splatPrefabs = new();

    void Start()
    {
        splatPrefabs["red"] = new GameObject[] { RedSplat_1, RedSplat_2 };
        splatPrefabs["blue"] = new GameObject[] { BlueSplat_1, BlueSplat_2 };
        splatPrefabs["yellow"] = new GameObject[] { YellowSplat_1, YellowSplat_2 };

        foreach (var color in splatPrefabs.Keys)
        {
            foreach (var prefab in splatPrefabs[color])
            {
                string key = color + "_" + prefab.name;
                splatPools[key] = new Queue<GameObject>();

                for (int i = 0; i < NumOfPooledObjects / 6; i++) // Evenly divide
                {
                    GameObject splat = Instantiate(prefab, Vector2.zero, Quaternion.identity, projectileParent);
                    splat.SetActive(false);
                    splatPools[key].Enqueue(splat);
                }
            }
        }
    }

    public void CreatedProjectilePublic(Vector2 hit)
    {
        CreateProjectile(hit);
    }

    GameObject CreateProjectile(Vector2 hit)
    {
        GameObject equippedCan = inventorySystem.GetEquippedSprayCan();
        if (equippedCan == null)
        {
            Debug.LogWarning("No spray can equipped!");
            return null;
        }
        string inkColor = equippedCan.GetComponent<CollectSpraycan>().colorName.ToLower();


        if (!splatPrefabs.ContainsKey(inkColor))
        {
            Debug.LogWarning("Unknown inkColor: " + inkColor);
            inkColor = "red";
        }

        // Randomly choose a variation (0 or 1)
        int index = Random.Range(0, 2);
        GameObject prefab = splatPrefabs[inkColor][index];
        string poolKey = inkColor + "_" + prefab.name;

        GameObject splat;

        if (splatPools[poolKey].Count > 0)
        {
            splat = splatPools[poolKey].Dequeue();
        }
        else
        {
            splat = Instantiate(prefab, Vector2.zero, Quaternion.identity, projectileParent);
        }

        splat.transform.position = hit;
        splat.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        splat.SetActive(true);

        // Optional: Automatically disable after time (if needed)
        // StartCoroutine(ReturnAfterSeconds(splat, poolKey, 5f));

        return splat;
    }

    public void ReturnSplat(GameObject splat, string color, GameObject prefab)
    {
        string key = color + "_" + prefab.name;
        splat.SetActive(false);
        splatPools[key].Enqueue(splat);
    }

    // Optional method if you want to auto-return after X seconds
    IEnumerator ReturnAfterSeconds(GameObject splat, string key, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        splat.SetActive(false);
        splatPools[key].Enqueue(splat);
    }
}




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code review : you didn't apply the code review comments from last time.
// Reminder : instead of a "pooling system" class, you should have a game object pool that
// extends unity's native pooling system, and then create instances of each pool per "type" of object
// example : ProjectilePool, RabbitPool, SandwichPool...
public class PoolingSystem : MonoBehaviour
{
    List<GameObject> activeProjectiles;
    Queue<GameObject> projectilePool;
    [SerializeField] Transform projectileParent;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] SprayParticles sprayParticles;
    [SerializeField] int NumOfPooledObjects = 10;

    [SerializeField] GameObject RedSplat_1;
    [SerializeField] GameObject RedSplat_2;
    [SerializeField] GameObject BlueSplat_1;
    [SerializeField] GameObject BlueSplat_2;
    [SerializeField] GameObject YellowSplat_1;
    [SerializeField] GameObject YellowSplat_2;
    [SerializeField] InventorySystem inventorySystem; // to tell which color is currently on, so which splats to leave

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
*/