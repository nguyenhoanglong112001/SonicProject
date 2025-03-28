using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpringCollect : MonoBehaviour
{
    [SerializeField] private Rigidbody rig;
    [SerializeField] private float force;
    [SerializeField] private float fouceUpRail;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform startpoint;
    [SerializeField] private Transform endpoint;
    [SerializeField] private float speedtoeb;
    [SerializeField] private LeanGameObjectPool pool;
    [SerializeField] private SwitchBall checkBall;
    private Vector3 currentpos;
    private Vector3 lastpos;

    public Transform Endpoint { get => endpoint; set => endpoint = value; }

    private void Start()
    {
        pool = GameObject.FindWithTag("PowerUpPool").GetComponent<LeanGameObjectPool>();
    }

    private void Update()
    {
        currentpos = transform.position;
        if (currentpos.y >= lastpos.y)
        {
            lastpos = currentpos;
        }
        else
        {
            startpoint = gameObject.transform;
            if (endpoint != null && CollectManager.instance.IsSpring)
            {
                anim.SetBool("Spring", true);
                transform.position = Vector3.MoveTowards(startpoint.position, endpoint.position, speedtoeb * Time.deltaTime);
                if (Vector3.Distance(transform.position, endpoint.position) < 0.1f)
                {
                    anim.SetBool("Spring", false);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Spring"))
        {
            ComboManager.instance.UpdateCombo();
            UIIngameManager.instance.ShowCombotype("Spring");
            CollectManager.instance.IsSpring = true;
            //if (checkBall.isball)
            //{
            //    checkBall.SwitchToCharacter();
            //}
            if (other.gameObject.GetComponent<SpringObject>().SpringGap && CollectManager.instance.IsSpring)
            {
                anim.SetBool("Spring", true);
                rig.AddForce(Vector3.up * force * Time.deltaTime, ForceMode.Impulse);
            }
            else if (other.gameObject.GetComponent<SpringObject>().Springeb && CollectManager.instance.IsSpring)
            {

                lastpos = transform.position;
                anim.SetBool("Spring", true);
                rig.AddForce(Vector3.up * fouceUpRail * Time.deltaTime, ForceMode.Impulse);
            }
            pool.Despawn(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(CollectManager.instance.IsSpring)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                anim.SetBool("Spring", false);
                anim.SetBool("IsFalling", false);
                CollectManager.instance.IsSpring = false;
            }    
        }
    }
}
