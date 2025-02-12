using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLessMap : MonoBehaviour
{
    [SerializeField] private GameObject map;
    [SerializeField] private Transform point;
    private GameObject mapspawn;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(mapspawn == null)
            {
                mapspawn = Instantiate(map, point.position,Quaternion.identity);
            }
        }
    }
}
