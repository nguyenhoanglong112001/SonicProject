using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTunnel : MonoBehaviour
{
    [SerializeField] private GameObject tunnel;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Vector3 rotation;
    void Start()
    {
        Instantiate(tunnel, spawnPos.position, Quaternion.Euler(rotation));
    }
}
