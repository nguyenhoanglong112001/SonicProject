using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class CollectPowerUp : MonoBehaviour
{
    [SerializeField] private LeanGameObjectPool powerPool;
    [SerializeField] private SpawnPowerUp currentPower;
    [SerializeField] private GameObject[] railprefab;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private PlayerStateManager player;
    private GameObject enerbeamRail;
    // Start is called before the first frame update
    void Start()
    {
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
                    SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.pickUpMagetSound);
                    CollectManager.instance.Ismaget = true;
                }
                else if (currentPower.PowerSpawn.CompareTag("Shield"))
                {
                    SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.pickUpPowerSound);
                    player.ShieldVFX.SetActive(true);
                    player.ShieldVFX.GetComponent<ParticleSystem>().Play();
                    foreach(Transform vfx in player.ShieldVFX.transform)
                    {
                        vfx.gameObject.GetComponent<ParticleSystem>().Play();
                    }
                    CollectManager.instance.SetShield(true);
                }
                else if (currentPower.PowerSpawn.CompareTag("Ring10"))
                {
                    SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.pickUpPowerSound);
                    CollectManager.instance.SetRing(10);
                }
                else if (currentPower.PowerSpawn.CompareTag("Ring20"))
                {
                    SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.pickUpPowerSound);
                    CollectManager.instance.SetRing(20);
                }
                else if (currentPower.PowerSpawn.CompareTag("RedRing"))
                {
                    SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.pickUpPowerSound);
                    CollectManager.instance.SetRedStartRing(1);
                }
                else if (currentPower.PowerSpawn.CompareTag("EnerbeamPickup"))
                {
                    SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.pickUpPowerSound);
                    CollectManager.instance.Isenerbeam = true;
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
                    SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.pickUpMagetSound);
                    CollectManager.instance.IsOrbMaget = true;
                }
                else if (currentPower.PowerSpawn.CompareTag("DoubleMutiply"))
                {
                    SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.pickUpPowerSound);
                    CollectManager.instance.IsDouble = true;
                }
                powerPool.Despawn(gameObject);
            }
        }
    }
}
