using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enerbeam : MonoBehaviour
{
    [SerializeField] private Transform startpoint;
    [SerializeField] private GameObject endpoint;
    [SerializeField] private SplineContainer splines;
    [SerializeField] private Grind checkrail;
    [SerializeField] private CollectManager checkcollect;
    [SerializeField] private float speedtorail;
    [SerializeField] private float progress = 0;
    [SerializeField] private float speed;
    [SerializeField] private Animator playeranimator;
    // Start is called before the first frame update
    void Start()
    {
        startpoint = gameObject.transform;
        checkrail = GameObject.FindWithTag("Player").GetComponent<Grind>();
        checkcollect = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
        playeranimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        endpoint = GameObject.FindWithTag("EnerbeamRail");
    }

    // Update is called once per frame
    void Update()
    {
        MoveToRail();
        GrindRail();
    }

    private void MoveToRail()
    {
        if (Vector3.Distance(transform.position,endpoint.transform.position) > 0.01f)
        {
            playeranimator.SetTrigger("StartEnerbeam");
            transform.position = Vector3.MoveTowards(startpoint.position, endpoint.transform.position, speedtorail * Time.deltaTime);
        }
    }

    private void GrindRail()
    {

        if (splines != null && checkrail.Israil)
        {
            progress += speed * Time.deltaTime;
            progress = Mathf.Clamp01(progress);
            Vector3 pos = splines.EvaluatePosition(progress);
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("StartRail"))
        {
            progress = 0;
            playeranimator.SetBool("Enerbeam", true);
            splines = other.gameObject.GetComponentInParent<SplineContainer>();
            checkrail.Israil = true;
        }    
        if(other.CompareTag("EndRail"))
        {
            splines = null;
            checkrail.Israil = false;
            checkrail.Isfalling = true;
            checkcollect.Isenerbeam = false;
            playeranimator.SetBool("Enerbeam", false);
            playeranimator.SetBool("IsFalling", true);
            playeranimator.SetBool("EnerRight", false);
            playeranimator.SetBool("EnerLeft", false);
            Destroy(other.transform.parent.gameObject,3.0f);
        }
    }
}
