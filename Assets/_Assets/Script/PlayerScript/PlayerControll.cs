using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private Animator playeranimator;
    [SerializeField] private Rigidbody playerrigi;
    [SerializeField] private float knock;
    [SerializeField] private InputManager ballcheck;
    [SerializeField] private DashPower checkdash;
    public bool isalive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Death(string parameter)
    {
        playeranimator.SetTrigger(parameter);
        playerrigi.velocity = Vector3.forward * knock * -1 * Time.deltaTime;
        isalive = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Blocker"))
        {
            if(!checkdash.isdashing)
            {
                if (ballcheck.isball)
                {
                    ballcheck.SwitchToCharacter();
                    Death("Death1");
                }
                Death("Death1");
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
