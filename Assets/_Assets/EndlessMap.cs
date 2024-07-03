using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMap : MonoBehaviour
{
    [SerializeField] private GameObject[] Map;
    [SerializeField] private Transform spawnposition;
    private GameObject map;
    int r;
    // Start is called before the first frame update
    void Start()
    {
        r = Random.Range(0, Map.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnWall()
    {
        if(map == null)
        {
            map = Instantiate(Map[r], spawnposition.position, Quaternion.identity);
        }    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SpawnWall();
        }
    }
}
