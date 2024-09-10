using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class MagetCollect : MonoBehaviour
{
    [SerializeField] private CollectManager checkmaget;
    [SerializeField] private LeanGameObjectPool objPool;
    [SerializeField] private PoolTag pooltag;
    // Start is called before the first frame update
    void Start()
    {
        checkmaget = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
        objPool = GameObject.FindWithTag(pooltag.ToString()).GetComponent<LeanGameObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            checkmaget.SetMaget(true);
            objPool.Despawn(gameObject);
        }    
    }
}
