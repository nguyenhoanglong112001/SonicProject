using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class DoubleObject : MonoBehaviour
{
    private CollectManager checkCollect;
    [SerializeField] private float timeCD;
    [SerializeField] private LeanGameObjectPool objectPool;
    [SerializeField] private PoolTag pooltag;
    // Start is called before the first frame update
    void Start()
    {
        checkCollect = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
        objectPool = GameObject.FindWithTag(pooltag.ToString()).GetComponent<LeanGameObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            checkCollect.IsDouble = true;
            objectPool.Despawn(gameObject);
        }    
    }
}
