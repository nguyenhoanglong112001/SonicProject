using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpringObject : MonoBehaviour
{
    [SerializeField] private bool springGap;
    [SerializeField] private bool springeb;
    [SerializeField] private GameObject[] ebRailPrefab;
    [SerializeField] private Transform pos;
    [SerializeField] private SpringCollect setendpoint;
    private GameObject ebrail;
    private int a;

    public bool SpringGap { get => springGap; set => springGap = value; }
    public bool Springeb { get => springeb; set => springeb = value; }

    private void Start()
    {
        setendpoint = GameObject.FindWithTag("Player").GetComponent<SpringCollect>();
        pos.position = new Vector3(0, pos.position.y, pos.position.z);
        a = Random.Range(0, ebRailPrefab.Length - 1);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if(other.gameObject.GetComponentInParent<Road>().type == TypeRoad.Road || other.gameObject.GetComponentInParent<Road>().type == TypeRoad.HillDown || other.gameObject.GetComponentInParent<Road>().type == TypeRoad.HillDown)
            {
                springeb = true;
                springGap = false;
            }
            else if (other.gameObject.GetComponentInParent<Road>().type == TypeRoad.GapRoad)
            {
                springGap = true;
                springeb = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (springeb)
            {
                if (ebrail == null)
                {
                    Debug.Log("SpawnRail");
                    ebrail = Instantiate(ebRailPrefab[a], pos.position, ebRailPrefab[a].transform.rotation);
                    setendpoint.Endpoint = ebrail.gameObject.GetComponent<Transform>();
                }
            }
        }
    }
}
