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
    public bool israil;
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
        Debug.Log("stay" + other.name);
        if (other.CompareTag("rail"))
        {
            israil = true;
            playeranimator.SetBool("Grind", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter" + other.name);
        if (other.CompareTag("rail"))
        {
            splineContain = other.gameObject.GetComponent<SplineContainer>();
            playeranimator.SetTrigger("StartGrind");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit" + other.name);
        if (other.CompareTag("rail"))
        {
            splineContain = null;
            playeranimator.SetBool("Grind", false);
            israil = false;
        }
    }

    public bool CheckRail()
    {
        return israil;
    }
}
