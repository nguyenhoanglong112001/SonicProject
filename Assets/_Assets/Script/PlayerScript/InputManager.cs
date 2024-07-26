using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Rigidbody playerrigi;
    [SerializeField] private float speed;
    [SerializeField] private float jumpforce;
    [SerializeField] private float lanedistance;
    [SerializeField] private float fallSpeed;
    [SerializeField] private Animator playeranimator;
    [SerializeField] private SwitchBall switchcheck;
    [SerializeField] private float timeroll;
    [SerializeField] private float distancetouch;
    [SerializeField] private Grind check;
    [SerializeField] private DashPower checkdash;
    [SerializeField] private CollectManager checkcollect;
    [SerializeField] private DashPad checkpad;

    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask raillayer;
    private float minspeed;
    private int lane = 0; //0 = mid ; -1 = left ; 1 = right;
    private Vector3 startpoint;
    private Vector3 endpoint;
    private bool iscrouching;
    private bool isjumping;
    private bool isball;


    public bool Iscrouching { get => iscrouching; set => iscrouching = value; }
    public bool Isball { get => isball; set => isball = value; }
    public bool Isjumping { get => isjumping; set => isjumping = value; }

    // Start is called before the first frame update
    void Start()
    {
        minspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!check.Isfalling && !checkcollect.Isenerbeam)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }    
        else
        {
            playerrigi.velocity = Vector3.down * fallSpeed * Time.deltaTime;
        }
        if(GroundCheck() && check.Isfalling)
        {
            check.Isfalling = false;
            playeranimator.SetBool("IsFalling", false);
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
            if(distancetouch < Vector3.Distance(endpoint,startpoint))
            {
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
                    if(!checkdash.isdashing)
                    {
                        if (deltalY > 0 && GroundCheck())
                        {
                            Isjumping = true;
                            playeranimator.SetTrigger("Roll");
                            playerrigi.AddForce(Vector3.up * jumpforce);
                            isball = true;
                        }
                        else
                        {
                            playeranimator.SetTrigger("Roll");
                            playerrigi.velocity = Vector3.down * jumpforce * Time.deltaTime;
                            Crouch();
                        }
                    }
                }
            }    
        }
    }


    IEnumerator ChangeCrouch()
    {
        switchcheck.ChangeBall();
        iscrouching = true;
        yield return new WaitForSeconds(timeroll);
        switchcheck.SwitchToCharacter();
        iscrouching = false;
        isball = false;
    }

    private void Crouch()
    {
        StartCoroutine(ChangeCrouch());
    }

    public bool GroundCheck()
    {
        bool isGround = Physics.Raycast(transform.position, Vector3.down, 1.0f, groundlayer);

        if(!isGround)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down, 1.1f, raillayer);
        }

        return isGround;
    }

    public void SpeedUp(float mutilpl)
    {
        speed *= mutilpl;
        if(speed < minspeed)
        {
            speed = minspeed;
        }
    }

    public int CheckLane()
    {
        return lane;
    }
}
