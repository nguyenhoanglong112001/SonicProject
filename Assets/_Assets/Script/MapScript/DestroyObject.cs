using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private LeanGameObjectPool objPool;
    [SerializeField] private PoolTag pooltag;
    // Start is called before the first frame update
    void Start()
    {
        objPool = GameObject.FindWithTag(pooltag.ToString()).GetComponent<LeanGameObjectPool>();
        objPool.Despawn(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
