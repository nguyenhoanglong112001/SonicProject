using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollectRing : MonoBehaviour
{
    [SerializeField] private List<GameObject> Ring;
    [SerializeField] private List<GameObject> Orb;
    [SerializeField] private Transform playerposition;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AutoCollect(Ring);
        AutoCollect(Orb);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(CollectManager.instance.Ismaget)
        {
            if (other.CompareTag("Ring"))
            {
                Ring.Add(other.gameObject);
            }
        }
        else if (CollectManager.instance.IsOrbMaget)
        {
            if(other.CompareTag("Orb"))
            {
                Orb.Add(other.gameObject);
            }
        }
    }

    private void AutoCollect(List<GameObject> collectList)
    {
        foreach(var obj in collectList)
        {
            if(obj != null)
            {
                obj.transform.position=Vector3.MoveTowards(obj.transform.position, playerposition.position, speed * Time.deltaTime);
            }
        }
    }
}
