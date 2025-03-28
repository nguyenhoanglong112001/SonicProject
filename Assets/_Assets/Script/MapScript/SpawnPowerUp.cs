using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject[] powerList;
    [SerializeField] private GameObject powerSpawn;

    public GameObject PowerSpawn { get => powerSpawn; set => powerSpawn = value; }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPower();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void SpawnPower()
    {
        int r = Random.Range(0, 100);
        if (r > 0 && r <98)
        {
            int a = Random.Range(0, powerList.Length - 2);
            if(powerList[a].GetComponent<PowerType>().typed == TypePower.Shield)
            {
                if(!CollectManager.instance.CheckShield())
                {
                    ActivePowerPickUp(powerList[a]);
                }
            }    
            else if (powerList[a].GetComponent<PowerType>().typed == TypePower.Magnet)
            {
                if(!CollectManager.instance.Ismaget)
                {
                    ActivePowerPickUp(powerList[a]);
                }
            }
            else if (powerList[a].GetComponent<PowerType>().typed == TypePower.OrbMagnet)
            {
                if(!CollectManager.instance.IsOrbMaget)
                {
                    ActivePowerPickUp(powerList[a]);
                }
            }
            else
            {
                ActivePowerPickUp(powerList[a]);
            }
            PowerSpawn = powerList[a];
        }
        else if (r > 98)
        {
            ActivePowerPickUp(powerList[6]);
            powerSpawn = powerList[6];
        }
    }
    
    private void ActivePowerPickUp(GameObject power)
    {
        foreach(GameObject obj in powerList)
        {
            if(obj != power)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }
}
