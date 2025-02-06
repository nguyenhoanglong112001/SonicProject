using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class EnergyOrbCollect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CollectManager check;
    [SerializeField] private float dashenergy;
    [SerializeField] private PlayerStateManager checkdash;
    [SerializeField] private GameObject ring;
    [SerializeField] private LeanGameObjectPool collectPool;
    void Start()
    {
        collectPool = GameObject.FindWithTag("CollectablePool").GetComponent<LeanGameObjectPool>();
        check = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
        checkdash = GameObject.FindWithTag("Player").GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (check.Energydash < 100 && checkdash.currentState is not DashState)
            {
                check.Energydash += dashenergy;
            }    
            collectPool.Despawn(gameObject);
        }
    }
}
