using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCam : MonoBehaviour
{
    [SerializeField] private Camera MainCam;
    [SerializeField] private Camera enerbeamCam;
    //[SerializeField] private PlayerStateManager player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(/*player.currentState is GrindState && */CollectManager.instance.Isenerbeam)
        {
            MainCam.enabled = false;
            enerbeamCam.enabled = true;
        }
        else if (!CollectManager.instance.Isenerbeam)
        {
            MainCam.enabled = true;
            enerbeamCam.enabled = false;
        }
    }
}
