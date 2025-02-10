 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnRoad : MonoBehaviour
{
    [SerializeField] private LeanGameObjectPool roadPool;
    [SerializeField] private GameObject roadZone1;
    [SerializeField] private CheckLane lane;
    [SerializeField] private float laneKey;
    private GameObject road;
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
        roadPool.Prefab = roadZone1;
        road = roadPool.Spawn(transform.position, roadZone1.transform.rotation, gameObject.transform);
        if (road != null)
        {
            lane.AddValueToDict(laneKey, road);
        }
    }
}
