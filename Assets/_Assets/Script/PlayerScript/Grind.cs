using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Grind : MonoBehaviour
{
    [SerializeField] private Animator playeranimator;
    [SerializeField] private SplineContainer splineContain;
    [SerializeField] private float speed;
    [SerializeField] private float progress;
    [SerializeField] private int currentrail;
    [SerializeField] private List<SplineContainer> splines;
    [SerializeField] private InputManager lane;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CollectManager check;
    [SerializeField] private float downspeed;
    [SerializeField] private bool isfalling;
    [SerializeField] private float offset;
    private bool israil;
    private bool istrigger;

    public SplineContainer SplineContain { get => splineContain; set => splineContain = value; }
    public bool Isfalling { get => isfalling; set => isfalling = value; }
    public bool Israil { get => israil; set => israil = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GrindRail();
    }

    private void GrindRail()
    {
        if (splineContain != null && israil)
        {
            progress += speed * Time.deltaTime;
            progress = Mathf.Clamp01(progress);
            Vector3 pos = splineContain.EvaluatePosition(progress);
            pos.y += offset;
            transform.position = pos;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("rail") && istrigger)
        {
            israil = true;
            playeranimator.SetBool("Grind", true);
            if (lane.CheckLane() == -1)
            {
                splineContain = splines[0];
            }
            else if (lane.CheckLane() == 0)
            {
                splineContain = splines[1];
            }
            else if (lane.CheckLane() == 1)
            {
                splineContain = splines[2];
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("rail"))
        {
            progress = 0;
            playeranimator.SetTrigger("StartGrind");
            istrigger = true;
            for (int i = 0; i < other.transform.childCount; i++)
            {
                splines.Add(other.transform.GetChild(i).GetComponent<SplineContainer>());
            }
            if (lane.CheckLane() == -1)
            {
                splineContain = splines[0];
            }
            else if (lane.CheckLane() == 0)
            {
                splineContain = splines[1];
            }
            else if (lane.CheckLane() == 1)
            {
                splineContain = splines[2];
            }
        }
        if(other.CompareTag("EnerbeamPickup"))
        {
            playeranimator.SetTrigger("StartEnerbeam"); 
        }
        if(other.CompareTag("EndRail"))
        {
            splineContain = null;
            playeranimator.SetBool("Grind", false);
            israil = false;
            istrigger = false;
        }    
    }
}
