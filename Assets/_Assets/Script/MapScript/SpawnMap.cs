using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;


public class SpawnMap : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnList;
    [SerializeField] private GameObject[] mapList;
    [SerializeField] private GameObject[] railList;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private LeanGameObjectPool roadPool;
    [SerializeField] private Transform objectParent;
    private GameObject player;
    private int a;
    private int lengthList = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
            int r = Random.Range(0, 100);
            Transform objrotation;
            if( r <=90)
            {
                a = Random.Range(0, mapList.Length - 1);
                roadPool.Prefab = mapList[a];
                objrotation = mapList[a].transform;
            }
            else
            {
                a = Random.Range(0, railList.Length - 1);
                roadPool.Prefab = railList[a];
                objrotation = railList[a].transform;
            }

            if(i==0)
            {
                spawnList[i] = roadPool.Spawn(spawnPos.position, objrotation.rotation,objectParent);
                spawnList[i].transform.localPosition =Vector3.zero ;
            }
            else
            {
                Transform pos = spawnList[i-1].transform.GetChild(0);
                spawnList[i] = roadPool.Spawn(pos.position, objrotation.rotation, objectParent);
            }
            StartCoroutine(RotateObject());
        }
    }   

    IEnumerator RotateObject()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.rotation = player.transform.rotation;
    }
}
