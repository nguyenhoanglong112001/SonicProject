using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCam : MonoBehaviour
{
    [SerializeField] private GameObject MainCam;
    [SerializeField] private GameObject enerbeamCam;
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
            MainCam.SetActive(false);
            enerbeamCam.SetActive(true);
            enerbeamCam.transform.SetParent(gameObject.transform);
        }
        else if (!checkrail.Israil && !checkcollect.Isenerbeam)
        {
            MainCam.SetActive(true);
            enerbeamCam.SetActive(false);
            enerbeamCam.transform.SetParent(null);
        }
    }
}
