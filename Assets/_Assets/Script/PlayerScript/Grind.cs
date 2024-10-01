using System;
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
    [SerializeField] private SwitchBall switchcheck;
    [SerializeField] private bool isfalling;
    [SerializeField] private float offset;
    [SerializeField] private float fallSpeed;
    [SerializeField] private SpawnMap checkRoad;
    [SerializeField] private Vector3 newpos;
    private Dictionary<float, GameObject> rail;
    private bool israil;

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
        if(israil && !check.Isenerbeam)
        {
            ChangeRail(lane.CheckLane());
        }    
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
        if (other.CompareTag("SpringRail"))
        {
            if(splines.Count > 0)
            {
                israil = true;
                playeranimator.SetBool("Grind", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            checkRoad = other.GetComponentInParent<SpawnMap>();
        }
        if(other.CompareTag("EnerbeamPickup"))
        {
            switchcheck.SwitchToCharacter();
            playeranimator.SetTrigger("StartEnerbeam"); 
        }
        if (other.CompareTag("EndRail"))
        {
            if(israil)
            {
                splines.Clear();
                splineContain = null;
                gameObject.transform.position += newpos;
                playeranimator.SetBool("Grind", false);
                israil = false;
            }    
            if(check.IsSpring)
            {
                splines.Clear();
                splineContain = null;
                playeranimator.SetBool("Grind", false);
                israil = false;
                isfalling = true;
                playeranimator.SetBool("IsFalling", true);
                playeranimator.SetBool("Spring", false);
                check.IsSpring = false;
                Destroy(other.gameObject.transform.parent.gameObject.transform.parent.gameObject, 1.0f);
            }
        }
        if (other.CompareTag("StartRail"))
        {
            if (!israil)
            {
                playeranimator.SetTrigger("StartGrind");
            }
            israil = true;
            switchcheck.SwitchToCharacter();
            progress = 0;
            playeranimator.SetBool("Grind", true);
            splineContain = other.gameObject.transform.parent.gameObject.GetComponentInChildren<SplineContainer>();
            GameObject objparent = other.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject;
            Debug.Log(objparent);
            rail = objparent.GetComponent<CheckLane>().Road;
        }
    }

    private void ChangeRail(int currentrail)
    {
        splineContain = rail[currentrail].GetComponent<SplineContainer>();
    }    
}
