using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CollectPowerUp : MonoBehaviour
{
    [SerializeField] private CollectManager check;
    [SerializeField] private LeanGameObjectPool powerPool;
    [SerializeField] private SpawnPowerUp currentPower;
    [SerializeField] private GameObject[] railprefab;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private PlayerStateManager player;
    private GameObject enerbeamRail;
    // Start is called before the first frame update
    void Start()
    {
        check = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
        powerPool = GameObject.FindWithTag("PowerUpPool").GetComponent<LeanGameObjectPool>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currentPower.PowerSpawn != null)
        {
            if (other.CompareTag("Player"))
            {
                if (currentPower.PowerSpawn.CompareTag("Maget"))
                {
                    check.Ismaget = true;
                    Debug.Log(check.Ismaget);
                }
                else if (currentPower.PowerSpawn.CompareTag("Shield"))
                {
                    check.SetShield(true);
                }
                else if (currentPower.PowerSpawn.CompareTag("Ring10"))
                {
                    check.SetRing(10);
                }
                else if (currentPower.PowerSpawn.CompareTag("Ring20"))
                {
                    check.SetRing(20);
                }
                else if (currentPower.PowerSpawn.CompareTag("RedRing"))
                {
                    check.SetRedStartRing(1);
                }
                else if (currentPower.PowerSpawn.CompareTag("EnerbeamPickup"))
                {
                    check.Isenerbeam = true;
                    if (enerbeamRail == null)
                    {
                        int a = Random.Range(0, railprefab.Length - 1);
                        enerbeamRail = Instantiate(railprefab[a], spawnpoint.position, railprefab[a].transform.rotation);
                    }
                    player.newState = player.state.Enerbeam();
                    player.SwitchState(player.newState);
                }
                else if (currentPower.PowerSpawn.CompareTag("OrbMaget"))
                {
                    check.IsOrbMaget = true;
                }
                else if (currentPower.PowerSpawn.CompareTag("DoubleMutiply"))
                {
                    check.IsDouble = true;
                }
                powerPool.Despawn(gameObject);
            }
        }
    }
}
