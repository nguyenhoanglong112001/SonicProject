using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class RingCollect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CollectManager check;
    [SerializeField] private int ringsup;
    [SerializeField] private LeanGameObjectPool collectPool;
    void Start()
    {
        if (gameObject.GetComponent<PowerType>() != null)
        {
            collectPool = GameObject.FindWithTag("PowerUpPool").GetComponent<LeanGameObjectPool>();
        }
        else
        {
            collectPool = GameObject.FindWithTag("CollectablePool").GetComponent<LeanGameObjectPool>();
        }    
        check = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            check.SetRing(ringsup);
            collectPool.Despawn(gameObject);
        }
    }
}
