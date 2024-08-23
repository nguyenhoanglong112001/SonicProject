using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectable : MonoBehaviour
{
    [SerializeField] private GameObject ring;
    [SerializeField] private GameObject orb;
    [SerializeField] private GameObject[] spawnPosList;
    [SerializeField] private GameObject typeCheck;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            foreach(Transform grandchild in child)
            {
                Destroy(grandchild.gameObject);
            }    
        }
        Spawn();
        if (typeCheck.GetComponent<RoadType>().type == TypeRoad.GapRoad)
        {
            Debug.Log("GapRoad");
            SpawnStreak();
        }
        else
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn()
    {
        int r = Random.Range(0, 100);
        if(r>=70 && r <85)
        {
            int x = Random.Range(0, 100);
            if(x < 50)
            {
                CollectableSpawn(ring);
            }
            else
            {
                CollectableSpawn(orb);
            }
        }
        else if (r>85 && r<98)
        {
            for (int i = 0; i < spawnPosList.Length; i++)
            {
                if(i%2 == 0)
                {
                    Instantiate(ring, spawnPosList[i].transform.position, ring.transform.rotation, gameObject.transform);
                }
                else
                {
                    Instantiate(orb, spawnPosList[i].transform.position, orb.transform.rotation, gameObject.transform);
                }
            }
        }
    }

    private void SpawnStreak()
    {
        int r = Random.Range(0, 100);
        if (r >= 85)
        {
            CollectableSpawn(ring);
        }
    }

    private void CollectableSpawn(GameObject obj)
    {
        for (int i =0;i < spawnPosList.Length;i++)
        {
            Instantiate(obj, spawnPosList[i].transform.position, obj.transform.rotation, gameObject.transform);
        }
    }
}
