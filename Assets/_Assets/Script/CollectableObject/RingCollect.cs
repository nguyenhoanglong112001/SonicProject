using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class RingCollect : MonoBehaviour
{
    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CollectManager.instance.SetRing(ringsup);
            collectPool.Despawn(gameObject);
        }
    }
}
