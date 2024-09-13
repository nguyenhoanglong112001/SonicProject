using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;


public class SpawnMap : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnList;
    [SerializeField] private GameObject[] mapList;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private LeanGameObjectPool roadPool;
    [SerializeField] private Transform objectParent;
    private int lengthList = 3;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in objectParent)
        {
            if(child != null)
            {
                Destroy(child.gameObject);
            }    
        }    
        roadPool = GameObject.FindWithTag("RoadPool").GetComponent<LeanGameObjectPool>();
        spawnList = new GameObject[lengthList];
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        for (int i =0;i<spawnList.Length;i++)
        {
            int a = Random.Range(0, mapList.Length-1);
            roadPool.Prefab = mapList[a];
            if(i==0)
            {
                spawnList[i] = roadPool.Spawn(spawnPos.position, mapList[a].transform.rotation,objectParent);
                spawnList[i].transform.localPosition =Vector3.zero ;
            }
            else
            {
                Transform pos = spawnList[i-1].transform.GetChild(0);
                spawnList[i] = roadPool.Spawn(pos.position, mapList[a].transform.rotation, objectParent);
            }
        }
    }    
}
