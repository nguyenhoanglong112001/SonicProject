using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject[] powerList;
    [SerializeField] private GameObject[] specialPowerUp;
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
        if (r > 88 && r <98)
        {
            int a = Random.Range(0, powerList.Length - 1);
            Instantiate(powerList[a], transform.position, powerList[a].transform.rotation,gameObject.transform);
        } 
        else if (r >= 98)
        {
            int a = Random.Range(0, specialPowerUp.Length - 1);
            Instantiate(specialPowerUp[a], transform.position, specialPowerUp[a].transform.rotation, gameObject.transform);
        }
    }
}
