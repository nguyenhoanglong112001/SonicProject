using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnRail : MonoBehaviour
{
    private LeanGameObjectPool roadPool;
    [SerializeField] private GameObject railPrefab;
    [SerializeField] private CheckLane lane;
    [SerializeField] private float laneindex;
    // Start is called before the first frame update
    void Start()
    {
        roadPool = GameObject.FindWithTag("RoadPool").GetComponent<LeanGameObjectPool>();
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        roadPool.Prefab = railPrefab;
        GameObject rail =roadPool.Spawn(gameObject.transform.position, railPrefab.transform.rotation, gameObject.transform);
        lane.AddValueToDict(laneindex, rail);
    }
}
