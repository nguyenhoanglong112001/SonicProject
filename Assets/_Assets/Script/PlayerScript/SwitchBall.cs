using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBall : MonoBehaviour
{
    [SerializeField] private Animator playeranimator;
    [SerializeField] private GameObject sonicObj;
    [SerializeField] private GameObject ballObj;
    [SerializeField] private Avatar characterAvatar;
    [SerializeField] private Avatar ballAvatar;
    [SerializeField] private RuntimeAnimatorController ballanimator;
    [SerializeField] private RuntimeAnimatorController characteranimator;
    [SerializeField] private CapsuleCollider charactercollider;
    [SerializeField] private SphereCollider ballcollider;
    //[SerializeField] private InputManager checkcondition;
    [SerializeField] private PlayerStateManager checkcondition;
    [SerializeField] private Grind checkrail;
    [SerializeField] private CollectManager checkcollect;

    [SerializeField] private Rigidbody playerrigil;

    public bool isball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Switch(bool ischangeBall,RuntimeAnimatorController animator, Avatar avatar, bool active1, bool active2)
    {
        Vector3 currentPos = transform.position;
        ballObj.SetActive(ischangeBall);
        sonicObj.SetActive(!ischangeBall);
        playeranimator.runtimeAnimatorController = animator;
        playeranimator.avatar = avatar;
        ballcollider.enabled = active1;
        charactercollider.enabled = active2;
        transform.position = currentPos;
    }

    public void ChangeBall()
    {
        Switch(true,ballanimator, ballAvatar, true, false);
        isball = true;
    }

    public void SwitchToCharacter()
    {
        Switch(false,characteranimator, characterAvatar, false, true);
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
