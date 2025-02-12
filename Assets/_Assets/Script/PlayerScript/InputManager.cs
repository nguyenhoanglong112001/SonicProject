using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

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
    [SerializeField] private PlayerControll checkCondition;
    [SerializeField] private float tospringspeed;
    [SerializeField] private CheckLane getRoadDict;
    [SerializeField] private TypeRoad laneType;
    public int indexroad;
    private float minspeed;
    private int lane = 0; //0 = mid ; -1 = left ; 1 = right;
    private Vector3 startMousepoint;
    private Vector3 endMousepoint;
    private bool isjumping;
    private bool isball;

    [Header("Dotween")]
    public Transform[] path;
    public float moveduration;


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
            if(checkcollect.IsSpring)
            {
                transform.Translate(Vector3.forward * tospringspeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
        }
        else if ((check.Isfalling && !checkcollect.Isenerbeam) || ((check.Isfalling && !checkcollect.IsSpring)))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            playerrigi.linearVelocity = Vector3.down * fallSpeed * Time.deltaTime;
        }
        if(checkCondition.GroundCheck() && check.Isfalling)
        {
            playeranimator.SetBool("IsFalling", false);
            check.Isfalling = false;
        }
        MoveWayPoint();
        InputMove();
    }


    private void InputMove()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startMousepoint = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            endMousepoint = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
        {
            if(distancetouch < Vector3.Distance(endMousepoint,startMousepoint))
            {
                float deltalX = endMousepoint.x - startMousepoint.x;
                float deltalY = endMousepoint.y - startMousepoint.y;
                if (Mathf.Abs(deltalX) > Mathf.Abs(deltalY))
                {
                    if (deltalX > 0)
                    {
                        if (lane < 1 && CheckChangeLane(lane+1,lane) && !checkcollect.Isenerbeam)
                        {
                            if(checkCondition._canDodge)
                            {
                                checkCondition.ComboUpdate("Dodge");

                            }
                            lane++;
                        }
                    }
                    else
                    {
                        if (lane > -1 && CheckChangeLane(lane - 1,lane) && !checkcollect.Isenerbeam)
                        {
                            if (checkCondition._canDodge)
                            {
                                checkCondition.ComboUpdate("Dodge");

                            }
                            lane--;
                        }
                    }
                    Vector3 targetPosition = transform.position;
                    targetPosition.x = lane * lanedistance;
                    transform.position = targetPosition;
                }
                else
                {
                    if(!checkdash.isdashing && !check.Israil && !checkcollect.Isenerbeam)
                    {
                        if (deltalY > 0 && checkCondition.GroundCheck())
                        {
                            Isjumping = true;
                            isball = true;
                            playeranimator.SetTrigger("Roll");
                            playerrigi.AddForce(Vector3.up * jumpforce);
                        }
                        else
                        {
                            playeranimator.SetTrigger("Roll");
                            playerrigi.linearVelocity = Vector3.down * jumpforce * Time.deltaTime;
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
        yield return new WaitForSeconds(timeroll);
        switchcheck.SwitchToCharacter();
    }

    private void Crouch()
    {
        StartCoroutine(ChangeCrouch());
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

    private bool CheckChangeLane(int lanetarget,int currentlane)
    {
        if(getRoadDict != null)
        {
            if(getRoadDict.Road.ContainsKey(currentlane))
            {
                if (getRoadDict.Road[lanetarget].GetComponent<Road>().type == TypeRoad.Road)
                {
                    if (getRoadDict.Road.ContainsKey(lanetarget))
                    {
                        if (getRoadDict.Road[lanetarget].GetComponent<Road>().type == TypeRoad.Hillup || getRoadDict.Road[lanetarget].GetComponent<Road>().type == TypeRoad.HillDown || getRoadDict.Road[lanetarget].GetComponent<Road>().type == TypeRoad.GapRoad
                            || getRoadDict.Road[lanetarget].GetComponent<Road>().type == TypeRoad.TurnRoadL || getRoadDict.Road[lanetarget].GetComponent<Road>().type == TypeRoad.TurnRoadR)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            GameObject objParent = collision.gameObject.transform.parent.gameObject;
            getRoadDict = objParent.GetComponentInParent<CheckLane>();
            laneType = collision.gameObject.GetComponentInParent<Road>().type;
        }
    }

    private void MoveWayPoint()
    {
        if(checkCondition.isTurn && checkCondition.wayPoints != null)
        {
            //path = checkCondition.wayPoints;
            Vector3[] pointPath = System.Array.ConvertAll(path, t => t.position);
            if (pointPath[0] == transform.position)
            {
                List<Vector3> tempPath = new List<Vector3>(pointPath);
                tempPath.RemoveAt(0);
                pointPath = tempPath.ToArray();
            }
            transform.DOPath(pointPath, moveduration, PathType.CatmullRom, PathMode.Full3D)
                .SetEase(Ease.Linear)
                .SetLookAt(0.01f)
                .OnComplete(() =>
                {
                    path = null;
                });
            checkCondition.isTurn = false;
        }
    }
}
