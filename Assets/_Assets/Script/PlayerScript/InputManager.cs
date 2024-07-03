using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Rigidbody playerrigi;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    [SerializeField] private float lanedistance;
    [SerializeField] private Animator playeranimator;
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private Material ballmaterial;
    [SerializeField] private Material characterMaterial;
    [SerializeField] private Mesh ballmesh;
    [SerializeField] private Mesh charactermesh;
    [SerializeField] private Avatar characterAvatar;
    [SerializeField] private Avatar ballAvatar;
    [SerializeField] private RuntimeAnimatorController ballanimator;
    [SerializeField] private RuntimeAnimatorController characteranimator;
    [SerializeField] private CapsuleCollider charactercollider;
    [SerializeField] private SphereCollider ballcollider;
    [SerializeField] private float timeroll;

    [SerializeField] private LayerMask groundlayer;
    private int lane = 0; //0 = mid ; -1 = left ; 1 = right;
    private Vector3 startpoint;
    private Vector3 endpoint;
    private bool iscrouching;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(GroundCheck() == true && !iscrouching)
        {
            Switch(charactermesh, characterMaterial, characteranimator, characterAvatar, false, true);

        }
        else
        {
            Switch(ballmesh,ballmaterial,ballanimator,ballAvatar,true,false);
        }
        InputMove();
    }

    private void InputMove()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startpoint = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            endpoint = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log(lane);
            float deltalX = endpoint.x - startpoint.x;
            float deltalY = endpoint.y - startpoint.y;
            if (Mathf.Abs(deltalX) > Mathf.Abs(deltalY))
            {
                if (deltalX > 0)
                {
                    if (lane < 1)
                    {
                        lane++;
                    }
                }
                else
                {
                    if (lane > -1)
                    {
                        lane--;
                    }
                }
                Vector3 targetPosition = transform.position;
                targetPosition.x = lane * lanedistance;
                transform.position = targetPosition;
            }
            else
            {
                if(deltalY > 0 && GroundCheck())
                {
                    playeranimator.SetTrigger("Jump");
                    playerrigi.velocity = Vector3.up * jumpforce * Time.deltaTime;
                }
                else
                {
                    Crouch();
                }
            }
        }
    }


    IEnumerator ChangeCrouch()
    {
        Debug.Log("A");
        iscrouching = true;

        yield return new WaitForSeconds(timeroll);
        iscrouching = false;
    }

    private void Crouch()
    {
        StartCoroutine(ChangeCrouch());
    }

    private void Switch(Mesh meshchange, Material material, RuntimeAnimatorController animator, Avatar avatar, bool active1,bool active2)
    {
        mesh.sharedMesh = meshchange;
        mesh.material = material;
        playeranimator.runtimeAnimatorController = animator;
        playeranimator.avatar = avatar;
        ballcollider.enabled = active1;
        charactercollider.enabled = active2;
    }

    private bool GroundCheck()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.0f, groundlayer);
    }
}
