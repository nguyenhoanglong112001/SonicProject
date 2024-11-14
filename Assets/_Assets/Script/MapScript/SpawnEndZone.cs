using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEndZone : MonoBehaviour
{
    [SerializeField] private GameObject endZone;
    [SerializeField] private CheckLane checkLane;
    [SerializeField] private Vector3 localPos = new Vector3(0,0,-90);
    private GameObject endzone;
    // Start is called before the first frame update
    void Start()
    {
        checkLane = GetComponent<CheckLane>();
        if(checkLane.isEndZone)
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
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        endzone = Instantiate(endZone, transform.position, endZone.transform.rotation,gameObject.transform);
        endzone.transform.localPosition = localPos;
    }

}
