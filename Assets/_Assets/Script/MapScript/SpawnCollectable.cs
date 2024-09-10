using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnCollectable : MonoBehaviour
{
    [SerializeField] private GameObject ring;
    [SerializeField] private GameObject orb;
    [SerializeField] private GameObject[] spawnPosList;
    [SerializeField] private GameObject typeCheck;
    [SerializeField] private LeanGameObjectPool collectablePool;
    // Start is called before the first frame update
    void Start()
    {
        collectablePool = GameObject.FindWithTag("CollectablePool").GetComponent<LeanGameObjectPool>();
        if (typeCheck.GetComponent<RoadType>().type == TypeRoad.GapRoad)
        {
            SpawnStreak();
        }
        else
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Spawn()
    {
        int r = Random.Range(0, 100);
        if(r>=70 && r <85)
        {
            int x = Random.Range(0, 100);
            if(x < 50)
            {
                for (int i =0;i < spawnPosList.Length;i++)
                {
                    CollectableSpawn(ring,spawnPosList[i].transform);
                }    
            }
            else
            {
                for (int i = 0; i < spawnPosList.Length; i++)
                {
                    CollectableSpawn(orb, spawnPosList[i].transform);
                }
            }
        }
        else if (r>85 && r<98)
        {
            for (int i = 0; i < spawnPosList.Length; i++)
            {
                if(i%2 == 0)
                {
                    CollectableSpawn(ring,spawnPosList[i].transform);
                }
                else
                {
                    CollectableSpawn(orb, spawnPosList[i].transform);
                }
            }
        }
    }

    private void SpawnStreak()
    {
        int r = Random.Range(0, 100);
        if (r >= 85)
        {
            for (int i = 0; i < spawnPosList.Length; i++)
            {
                CollectableSpawn(ring, spawnPosList[i].transform);
            }
        }
    }

    private void CollectableSpawn(GameObject obj,Transform spawnPos)
    {
        collectablePool.Prefab = obj;
        GameObject collectSpawn = collectablePool.Spawn(spawnPos.position, obj.transform.rotation);
    }
}
