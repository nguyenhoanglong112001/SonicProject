using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject[] powerList;
    [SerializeField] private GameObject[] specialPowerUp;
    [SerializeField] private List<GameObject> spawnList;
    [SerializeField] private CollectManager checkCollect;
    private LeanGameObjectPool powerUpPool;
    // Start is called before the first frame update
    void Start()
    {
        powerUpPool = GameObject.FindWithTag("PowerUpPool").GetComponent<LeanGameObjectPool>();
        checkCollect = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
        SpawnPower();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SpawnPower()
    {
        int r = Random.Range(0, 100);
        if (r > 88 && r <98)
        {
            int a = Random.Range(0, powerList.Length - 1);
            if(powerList[a].GetComponent<PowerType>().typed == TypePower.Shield)
            {
                if(!checkCollect.CheckShield())
                {
                    powerUpPool.Prefab = powerList[a];
                    GameObject power= powerUpPool.Spawn(transform.position, powerList[a].transform.rotation);
                    spawnList.Add(power);
                }
            }    
            else if (powerList[a].GetComponent<PowerType>().typed == TypePower.Magnet)
            {
                if(!checkCollect.Ismaget)
                {
                    powerUpPool.Prefab = powerList[a];
                    GameObject power = powerUpPool.Spawn(transform.position, powerList[a].transform.rotation);
                    spawnList.Add(power);
                }
            }
            else
            {
                powerUpPool.Prefab = powerList[a];
                GameObject power = powerUpPool.Spawn(transform.position, powerList[a].transform.rotation);
                spawnList.Add(power);
            }   
        } 
        else if (r >= 98)
        {
            int a = Random.Range(0, specialPowerUp.Length);
            powerUpPool.Prefab = specialPowerUp[a];
            GameObject power = powerUpPool.Spawn(transform.position, specialPowerUp[a].transform.rotation);
            spawnList.Add(power);
        }
    }
}
