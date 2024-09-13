using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private PoolTag poolType;
    private LeanGameObjectPool pool;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(transform.gameObject);
        }
        pool = GameObject.FindWithTag(poolType.ToString()).GetComponent<LeanGameObjectPool>();
        ObjSpawn();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void ObjSpawn()
    {
        pool.Prefab = objPrefab;
        pool.Spawn(transform.position, objPrefab.transform.rotation,transform);
    }
}
