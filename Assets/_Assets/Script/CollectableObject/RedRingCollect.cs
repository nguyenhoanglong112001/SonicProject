using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class RedRingCollect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CollectManager check;
    [SerializeField] private int redringsup;
    [SerializeField] private LeanGameObjectPool powerUpPool;
    void Start()
    {
        powerUpPool = GameObject.FindWithTag("PowerUpPool").GetComponent<LeanGameObjectPool>();
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
            check.SetRedStartRing(redringsup);
            powerUpPool.Despawn(gameObject);
        }
    }
}
