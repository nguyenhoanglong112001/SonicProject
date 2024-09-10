using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTest : MonoBehaviour
{
    [SerializeField] private Rigidbody rig;
    [SerializeField] private float speed;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rig.AddForce(transform.forward * speed);
        if(Physics.Raycast(transform.position,-transform.up,out hit))
        {
            Vector3 surfaceNormal = hit.normal;
            transform.rotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;
        }    
    }
}
