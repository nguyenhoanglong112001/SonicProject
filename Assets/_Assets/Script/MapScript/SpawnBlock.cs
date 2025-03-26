using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Lean.Pool;

public class SpawnBlock : MonoBehaviour
{
    public BlockerType type;
    public GameObject block;
    List<int> listint;
    private LeanGameObjectPool blockPool;
    void Start()
    {
        foreach(Transform child in transform)
        {
            Destroy(transform.gameObject);
        }    
        blockPool = GameObject.FindWithTag("BlockPool").GetComponent<LeanGameObjectPool>();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Spawn()
    {
        blockPool.Prefab = SpawnManager.instance.GetBlocker(type);
        blockPool.Spawn(transform.position, SpawnManager.instance.GetBlocker(type).transform.rotation, transform);
    }
}
