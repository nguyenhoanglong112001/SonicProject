using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTunnel : MonoBehaviour
{
    [SerializeField] private GameObject tunnel;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Vector3 rotation;
    private GameObject tunnelSpawn;
    void Start()
    {
        if(tunnelSpawn == null)
        {
            tunnelSpawn = Instantiate(tunnel, spawnPos.position, Quaternion.Euler(rotation));
        }
    }
}
