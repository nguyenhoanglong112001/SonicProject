using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class EnergyOrbCollect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float dashenergy;
    [SerializeField] private PlayerStateManager checkdash;
    [SerializeField] private GameObject ring;
    [SerializeField] private LeanGameObjectPool collectPool;
    void Start()
    {
        collectPool = GameObject.FindWithTag("CollectablePool").GetComponent<LeanGameObjectPool>();
        checkdash = PlayerManager.instance.playerState;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        SoundManager.instance.PlaySound(SoundManager.instance.collectOrb, SoundManager.instance.collcetOrbSound);
        if (other.CompareTag("Player"))
        {
            if (checkdash.currentState is not DashState)
            {
                CollectManager.instance.UpdateEnergyDash(dashenergy);
                collectPool.Despawn(gameObject);
            }
        }
    }
}
