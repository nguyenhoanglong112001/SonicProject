using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] blocklist;
    [SerializeField] private GameObject[] spawnlist;
    [SerializeField] private int indexlast;
    [SerializeField] private int numberofBlock;
    [SerializeField] private MeshRenderer meshref;
    [SerializeField] private float lane;
    private int a;
    void Start()
    {
        spawnlist = new GameObject[numberofBlock];
        for (int i = 0;i<numberofBlock;i++)
        {
            a = Random.Range(0, 100);
            if (a > 90)
            {
                int r = Random.Range(0, indexlast);
                Bounds bounds = meshref.bounds;

                float randomZ = Random.Range(bounds.min.z, bounds.max.z);
                Vector3 spawnPos = new Vector3(lane, 0, randomZ);
                for (int j = 0; j < spawnlist.Length;j++)
                {
                    if(j != i)
                    {
                        if(spawnlist[j] != null)
                        {
                            while (Vector3.Distance(spawnPos,spawnlist[j].transform.position) <= 5)
                            {
                                randomZ = Random.Range(bounds.min.z, bounds.max.z);
                                spawnPos = new Vector3(lane, 0, randomZ);
                            }
                        }
                    }
                }
                GameObject spawnobj = Instantiate(blocklist[r], spawnPos, blocklist[r].transform.rotation);
                spawnlist[i] = spawnobj;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
