using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCam : MonoBehaviour
{
    [SerializeField] private Camera MainCam;
    [SerializeField] private Camera enerbeamCam;
    [SerializeField] private Grind checkrail;
    [SerializeField] private CollectManager checkcollect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkrail.Israil && checkcollect.Isenerbeam)
        {
            MainCam.enabled = false;
            enerbeamCam.enabled = true;
        }
        else if (!checkrail.Israil && !checkcollect.Isenerbeam)
        {
            MainCam.enabled = true;
            enerbeamCam.enabled = false;
        }
    }
}
