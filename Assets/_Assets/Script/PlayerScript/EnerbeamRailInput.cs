using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnerbeamRailInput : MonoBehaviour
{
    [SerializeField] private float sensitive;
    [SerializeField] private Grind checkrail;
    [SerializeField] private CollectManager check;
    [SerializeField] private Animator playeranima;
    [SerializeField] private float tiltthreshold;
    [SerializeField] private float AngleRotate;
    [SerializeField] private float maxrotate;
    [SerializeField] private float minrorate;
    private Vector3 acceleration;
    private float pitch;
    
    // Start is called before the first frame update
    void Start()
    {
        checkrail = GameObject.FindWithTag("Player").GetComponent<Grind>();
        check = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
        playeranima = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementOnRail  ();
    }

    private void MovementOnRail()
    {
        if(checkrail.Israil && check.Isenerbeam)
        {
            acceleration = Input.acceleration;
            float deltaPitch = acceleration.x * AngleRotate;
            pitch = Mathf.Clamp(pitch + deltaPitch, minrorate, maxrotate);
            transform.localEulerAngles= new Vector3(0, 0, pitch);
            if (acceleration.x > tiltthreshold)
            {
                playeranima.SetBool("EnerRight", true);
                playeranima.SetBool("EnerLeft", false);
            }
            else if (acceleration.x < tiltthreshold)
            {
                transform.Rotate(Vector3.forward);
                playeranima.SetBool("EnerRight", false);
                playeranima.SetBool("EnerLeft", true);
            }
            else if (acceleration.x == tiltthreshold)
            {
                transform.Rotate(Vector3.zero);
                playeranima.SetBool("EnerRight", false);
                playeranima.SetBool("EnerLeft", false);
            }    
        }
    }
}
