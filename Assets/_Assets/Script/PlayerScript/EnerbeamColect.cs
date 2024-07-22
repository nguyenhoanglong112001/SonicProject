using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnerbeamColect : MonoBehaviour
{
    [SerializeField] private Animator playeranimator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CollectManager checkcollect;
    [SerializeField] private GameObject enerbeamPrefab;
    [SerializeField] private Transform pos;
    [SerializeField] private GameObject splinepos;
    [SerializeField] private Transform grappoint;
    private GameObject ener;

    public GameObject Splinepos { get => splinepos; private set => splinepos = value; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        EnerbeamRide();
        if(grappoint != null)
        {
            transform.position = grappoint.transform.position;
        }
    }

    private void EnerbeamRide()
    {
        if(checkcollect.Isenerbeam)
        {
            if(ener == null)
            {
                ener = Instantiate(enerbeamPrefab, pos.position, enerbeamPrefab.transform.rotation);
                grappoint = ener.transform.GetChild(0).transform.GetChild(4);
                rb.useGravity = false;
            }
        }
        if(!checkcollect.Isenerbeam)
        {
            rb.useGravity = true;
            Destroy(ener);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnerbeamPickup"))
        {
            splinepos = other.gameObject.transform.GetChild(0).gameObject;
            rb.velocity = Vector3.zero;
        }
    }
}
