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
    public bool israil;
    private bool istrigger;
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
        if(splineContain != null && israil)
        {
            progress += speed * Time.deltaTime;
            progress = Mathf.Clamp01(progress);
            Vector3 pos = splineContain.EvaluatePosition(progress);
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
        if (other.CompareTag("rail"))
        {
            Debug.Log("enter");
            playeranimator.SetTrigger("StartGrind");
            istrigger = true;
            for (int i =0; i< other.transform.childCount;i++)
            {
                splines.Add(other.transform.GetChild(i).GetComponent<SplineContainer>());
            }
            if(lane.CheckLane() == -1)
            {
                splineContain = splines[0];
            }
            else if (lane.CheckLane() == 0)
            {
                splineContain = splines[1];
            }
            else if(lane.CheckLane() == 1)
            {
                splineContain = splines[2];
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("rail"))
        {
            splineContain = null;
            playeranimator.SetBool("Grind", false);
            israil = false;
            istrigger = false;
        }
    }

    public bool CheckRail()
    {
        return israil;
    }
}
