using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringObject : MonoBehaviour
{
    [SerializeField] private bool springGap;
    [SerializeField] private bool springeb;
    [SerializeField] private GameObject ebRailPrefab;
    [SerializeField] private Transform pos;
    [SerializeField] private SpringCollect setendpoint;
    private GameObject ebrail;

    public bool SpringGap { get => springGap; set => springGap = value; }
    public bool Springeb { get => springeb; set => springeb = value; }

    private void Start()
    {
        setendpoint = GameObject.FindWithTag("Player").GetComponent<SpringCollect>();
        pos.position = new Vector3(0, pos.position.y, pos.position.z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if(other.gameObject.GetComponentInParent<RoadType>().type == TypeRoad.Road || other.gameObject.GetComponentInParent<RoadType>().type == TypeRoad.HillDown || other.gameObject.GetComponentInParent<RoadType>().type == TypeRoad.HillDown)
            {
                springeb = true;
                springGap = false;
            }
            else if (other.gameObject.GetComponentInParent<RoadType>().type == TypeRoad.GapRoad)
            {
                springGap = true;
                springeb = false;
            }
        }
        if(other.CompareTag("Player"))
        {
            if(springeb)
            {
                if (ebrail == null)
                {
                    ebrail = Instantiate(ebRailPrefab, pos.position, ebRailPrefab.transform.rotation);
                    setendpoint.Endpoint = ebrail.gameObject.GetComponent<Transform>();
                }
            }
        }
    }
}