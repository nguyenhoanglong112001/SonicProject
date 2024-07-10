using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollectRing : MonoBehaviour
{
    [SerializeField] private CollectManager check;
    [SerializeField] private List<GameObject> Ring;
    [SerializeField] private Transform playerposition;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AutoCollect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ring"))
        {
            Ring.Add(other.gameObject);
        }
    }

    private void AutoCollect()
    {
        foreach(var ring in Ring)
        {
            if(ring!=null)
            {
                ring.transform.position=Vector3.MoveTowards(ring.transform.position, playerposition.position, speed * Time.deltaTime);
            }
        }
    }
}
