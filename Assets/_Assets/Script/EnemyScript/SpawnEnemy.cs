using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private float changeSpawn;
    // Start is called before the first frame update
    void Start()
    {
        EnemySpawn();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void EnemySpawn()
    {
        int r = Random.Range(0, 100);
        Debug.Log(r);
        {
            if (r > changeSpawn)
            {
                int a = Random.Range(0, enemyPrefab.Length);
                {
                    Instantiate(enemyPrefab[a], transform.position, enemyPrefab[a].transform.rotation);
                }
            }
        }
    }
}
