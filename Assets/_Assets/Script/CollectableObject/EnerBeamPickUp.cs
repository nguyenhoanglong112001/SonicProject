using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnerBeamPickUp : MonoBehaviour
{
    [SerializeField] private CollectManager checkcollect;
    [SerializeField] private GameObject railprefab;
    [SerializeField] private Transform spawnpoint;
    private GameObject enerbeamRail;
    // Start is called before the first frame update
    void Start()
    {
        checkcollect = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
        spawnpoint.position = new Vector3(0, spawnpoint.position.y, spawnpoint.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            checkcollect.Isenerbeam = true;
            if(enerbeamRail == null)
            {
                enerbeamRail = Instantiate(railprefab, spawnpoint.position,railprefab.transform.rotation);
            }
            Destroy(gameObject);
        }
    }

}
