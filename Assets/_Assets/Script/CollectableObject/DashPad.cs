using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPad : MonoBehaviour
{
    [SerializeField] private SwitchBall switchball;
    [SerializeField] private DashPower dashcheck;
    [SerializeField] private float dashpadspeed;
    [SerializeField] private float dashdistance;
    private Vector3 startpos;
    private bool isdashpad;

    public bool Isdashpad { get => isdashpad; set => isdashpad = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Dashpad"))
        {
            isdashpad = true;
            startpos = other.transform.position;
            switchball.ChangeBall();
            dashcheck.isdashing = true;
            switchball.isball = true;
            if (Vector3.Distance(transform.position,startpos) < dashdistance)
            {
                transform.Translate(Vector3.forward * dashpadspeed * Time.deltaTime);
            }
            else
            {
                isdashpad = false;
                switchball.isball = true;
                switchball.SwitchToCharacter();
                dashcheck.isdashing = false;
            }
        }
    }
}
