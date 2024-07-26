using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPad : MonoBehaviour
{
    [SerializeField] private SwitchBall switchball;
    [SerializeField] private DashPower dashcheck;
    [SerializeField] private float dashdistance;
    [SerializeField] private InputManager speed;
    private Vector3 startpos;
    public bool isdashpad;
    private bool istrigger;

    public bool Isdashpad { get => isdashpad; set => isdashpad = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isdashpad)
        {
            //Debug.Log(Vector3.Distance(transform.position, startpos));
            if (Vector3.Distance(transform.position, startpos) > dashdistance && istrigger)
            {
                Debug.Log("Abc");
                isdashpad = false;
                speed.SpeedUp(1/4);
                switchball.isball = false;
                switchball.SwitchToCharacter();
                dashcheck.isdashing = false;
                istrigger = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dashpad") && !istrigger)
        {
            switchball.ChangeBall();
            dashcheck.isdashing = true;
            switchball.isball = true;
            startpos = other.transform.position;
            if (Vector3.Distance(transform.position, startpos) < dashdistance)
            {
                speed.SpeedUp(4);
            }
            istrigger = true;
            isdashpad = true;
        }
    }
}
