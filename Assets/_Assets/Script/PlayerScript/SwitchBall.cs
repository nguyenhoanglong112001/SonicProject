using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBall : MonoBehaviour
{
    [SerializeField] private GameObject sonicObj;
    [SerializeField] private GameObject ballObj;
    [SerializeField] private SphereCollider charactercollider;
    [SerializeField] private SphereCollider ballcollider;
    [SerializeField] private PlayerStateManager checkcondition;

    public bool isball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Switch(bool ischangeBall, bool active1, bool active2)
    {
        Vector3 currentPos = transform.position;
        Quaternion currentRotate = transform.rotation;
        ballObj.SetActive(ischangeBall);
        sonicObj.SetActive(!ischangeBall);
        ballcollider.enabled = active1;
        charactercollider.enabled = active2;
        transform.position = currentPos;
        transform.rotation = currentRotate;
    }

    public void ChangeBall()
    {
        Switch(true,  true, false);
        isball = true;
    }

    public void SwitchToCharacter()
    {
        Switch(false, false, true);
        isball = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(checkcondition.isjump)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                SwitchToCharacter();
                checkcondition.isjump = false;
            }
        }
    }
}
