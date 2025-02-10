using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Lean.Pool;

public class SpawnBlock : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject blockZone1;
    [SerializeField] private GameObject blockZone2;
    [SerializeField] private GameObject blockZone3;
    [SerializeField] private GameObject blockZone4;
    [SerializeField] private Dictionary<Zone, GameObject> blockDict;
    List<int> listint;
    private LeanGameObjectPool blockPool;
    void Start()
    {
        foreach(Transform child in transform)
        {
            Destroy(transform.gameObject);
        }    
        blockPool = GameObject.FindWithTag("BlockPool").GetComponent<LeanGameObjectPool>();
        blockDict = new Dictionary<Zone, GameObject>()
        {
            {Zone.Zone1,blockZone1 },
            {Zone.Zone2,blockZone2 },
            {Zone.Zone4,blockZone4 }
        };
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Spawn()
    {
        if(blockDict.ContainsKey(ZoneManager.instance.currentZone))
        {
            blockPool.Prefab = blockDict[ZoneManager.instance.currentZone];
            blockPool.Spawn(transform.position, blockDict[ZoneManager.instance.currentZone].transform.rotation, transform);
        }    
    }
}
