using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnRoad : MonoBehaviour
{
    [SerializeField] private LeanGameObjectPool roadPool;
    [SerializeField] private GameObject roadZone1;
    [SerializeField] private GameObject roadZone2;
    [SerializeField] private GameObject roadZone3;
    [SerializeField] private GameObject roadZone4;
    [SerializeField] private ZoneManager currentZone;
    [SerializeField] private Dictionary<Zone, GameObject> roadDict;
    [SerializeField] private CheckLane lane;
    [SerializeField] private int laneKey;
    private GameObject road;
    // Start is called before the first frame update
    void Start()
    {
        roadPool = GameObject.FindWithTag("RoadPool").GetComponent<LeanGameObjectPool>();
        currentZone = GameObject.FindWithTag("Zone").GetComponent<ZoneManager>();
        roadDict = new Dictionary<Zone, GameObject>()
        {
            {Zone.Zone1,roadZone1 },
            {Zone.Zone2,roadZone2 },
            {Zone.Zone3,roadZone3 },
            {Zone.Zone4,roadZone4 }
        };
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        if(roadDict.ContainsKey(currentZone.currentZone))
        {
            roadPool.Prefab = roadDict[currentZone.currentZone];
            road = roadPool.Spawn(transform.position, roadDict[currentZone.currentZone].transform.rotation, gameObject.transform);
            lane.RoadDict.Add(laneKey, road.GetComponent<RoadType>().type);
        }
    }
}
