using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCollect : MonoBehaviour
{
    [SerializeField] private Rigidbody rig;
    [SerializeField] private float force;
    [SerializeField] private Animator anim;
    [SerializeField] private CollectManager checkCollect;
    [SerializeField] private Transform startpoint;
    [SerializeField] private Transform endpoint;
    [SerializeField] private float speedtoeb;

    public Transform Endpoint { get => endpoint; set => endpoint = value; }

    private void Start()
    {
    }

    private void Update()
    {
        if (endpoint != null && checkCollect.IsSpring)
        {
            anim.SetBool("Spring", true);
            transform.position = Vector3.MoveTowards(startpoint.position, endpoint.position, speedtoeb * Time.deltaTime);
            if(Vector3.Distance(transform.position,endpoint.position) < 0.1f)
            {
                anim.SetBool("Spring", false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Spring"))
        {
            checkCollect.IsSpring = true;
            if (other.gameObject.GetComponent<SpringObject>().SpringGap && checkCollect.IsSpring)
            {
                anim.SetBool("Spring", true);
                rig.AddForce(Vector3.up * force * Time.deltaTime, ForceMode.Impulse);
            }
            else if (other.gameObject.GetComponent<SpringObject>().Springeb && checkCollect.IsSpring)
            {
                startpoint = gameObject.transform;
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(checkCollect.IsSpring)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                anim.SetBool("Spring", false);
                anim.SetBool("IsFalling", false);
                checkCollect.IsSpring = false;
            }    
        }
    }
}
