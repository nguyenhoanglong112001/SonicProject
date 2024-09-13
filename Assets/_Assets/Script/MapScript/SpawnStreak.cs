using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnStreak : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private GameObject ring;
    [SerializeField] private GameObject orb;
    [SerializeField] private LeanGameObjectPool collectPool;
    void Start()
    {
        collectPool = GameObject.FindWithTag("CollectablePool").GetComponent<LeanGameObjectPool>();
        SpawnOnEbRail();
    }
    void Update()
    {
        
    }

    private void SpawnOnEbRail()
    {
        Bounds bounds = mesh.bounds;
        float spawnZ = bounds.min.z + 5;
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, spawnZ);
        while (spawnPos.z < bounds.max.z)
        {
            int a = Random.Range(0, 100);
            if(a <=50)
            {
                collectPool.Prefab = ring;
                collectPool.Spawn(spawnPos, ring.transform.rotation, gameObject.transform);
            }
            else
            {
                collectPool.Prefab = orb;
                collectPool.Spawn(spawnPos, ring.transform.rotation, gameObject.transform); 
            }
            spawnZ += 2;
            spawnPos = new Vector3(transform.position.x, transform.position.y, spawnZ);
        }
    }
}
