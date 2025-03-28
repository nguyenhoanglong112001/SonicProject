using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnCollectable : MonoBehaviour
{
    [SerializeField] private GameObject orb;
    [SerializeField] private GameObject ring;
    [SerializeField] private List<GameObject> spawnList;
    [SerializeField] private LeanGameObjectPool pool;
    [SerializeField] private Vector3 distanceSpawn;
    [SerializeField] private int a;
    [SerializeField] private bool isGapRoad;

    private void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
        if(a == 0)
        {
            a = Random.Range(4, 7);
        }
        pool = GameObject.FindWithTag("CollectablePool").GetComponent<LeanGameObjectPool>();
        Spawn();
    }

    private void Update()
    {
        
    }

    private void Spawn()
    {
        int r = Random.Range(0, 100);
        Vector3 pos = gameObject.transform.position;
        if (isGapRoad)
        {
            for (int i = 0; i < a; i++)
            {
                if(i <= (int)(a/2 +1))
                {
                    spawnList.Add(SpawnObject(ring, pos));
                    pos += distanceSpawn;
                }
                else
                {
                    spawnList.Add(SpawnObject(ring, pos));
                    pos -= distanceSpawn;
                }
            }
            return;
        }
        if (r < 60)
        {
            for (int i = 0; i < a; i++)
            {
                spawnList.Add(SpawnObject(ring, pos));
                pos += distanceSpawn;
            }
        }
        else
        {
            for (int i = 0; i < a; i++)
            {
                spawnList.Add(SpawnObject(orb, pos));
                pos += distanceSpawn;
            }
        }
    }

    private GameObject SpawnObject(GameObject objPrefab,Vector3 pos)
    {
        pool.Prefab = objPrefab;
        return pool.Spawn(pos,objPrefab.transform.rotation,gameObject.transform);
    }
}
