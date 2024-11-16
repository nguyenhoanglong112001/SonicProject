using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;
    public PlayerStateFactory state;

    [Header("Chi so chung")]
    [SerializeField] public PlayerControll checkCondition;
    [SerializeField] public Animator playeranimator;
    [SerializeField] public SwitchBall switchcheck;
    public Rigidbody playerrigi;
    [Header("Chi so chay")]
    public float speed;
    private Vector3 startMousepoint;
    private Vector3 endMousepoint;
    public float distancetouch;
    private int lane = 0; //0 = mid ; -1 = left ; 1 = right;
    public float lanedistance;
    public CheckLane getRoadDict;

    [Header("Chi so nhay")]
    public float jumpforce;

    [Header("bien bool chung")]
    public bool isball;
    public bool isjump;

    private void Awake()
    {
        state = new PlayerStateFactory(this);
        currentState = state.Run();
        currentState.EnterState(this);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        InputMove();
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void InputMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousepoint = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            endMousepoint = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (distancetouch < Vector3.Distance(endMousepoint, startMousepoint))
            {
                float deltalX = endMousepoint.x - startMousepoint.x;
                float deltalY = endMousepoint.y - startMousepoint.y;
                if (Mathf.Abs(deltalX) > Mathf.Abs(deltalY))
                {
                    if (deltalX > 0)
                    {
                        if (lane < 1 && CheckChangeLane(lane + 1, lane) /*&& !checkcollect.Isenerbeam*/)
                        {
                            //if (checkCondition._canDodge)
                            //{
                            //    checkCondition.ComboUpdate("Dodge");

                            //}
                            lane++;
                        }
                    }
                    else
                    {
                        if (lane > -1 && CheckChangeLane(lane - 1, lane) /*&& !checkcollect.Isenerbeam*/)
                        {
                            //if (checkCondition._canDodge)
                            //{
                            //    checkCondition.ComboUpdate("Dodge");
                            //}
                            lane--;
                        }
                    }
                    Vector3 targetPosition = transform.position;
                    targetPosition.x = lane * lanedistance;
                    transform.position = targetPosition;
                }
                else
                {
                    if (deltalY > 0 && checkCondition.GroundCheck())
                    {
                        currentState = state.Jump();
                        SwitchState(currentState);
                    }
                    else
                    {
                        playeranimator.SetTrigger("Roll");
                        playerrigi.velocity = Vector3.down * jumpforce * Time.deltaTime;
                        //Crouch();
                    }
                }
            }
        }
    }
    private bool CheckChangeLane(int lanetarget, int currentlane)
    {
        if (getRoadDict != null)
        {
            if (getRoadDict.Road.ContainsKey(currentlane))
            {
                if (getRoadDict.Road[lanetarget].GetComponent<RoadType>().type == TypeRoad.Road)
                {
                    if (getRoadDict.Road.ContainsKey(lanetarget))
                    {
                        if (getRoadDict.Road[lanetarget].GetComponent<RoadType>().type == TypeRoad.Hillup || getRoadDict.Road[lanetarget].GetComponent<RoadType>().type == TypeRoad.HillDown || getRoadDict.Road[lanetarget].GetComponent<RoadType>().type == TypeRoad.GapRoad
                            || getRoadDict.Road[lanetarget].GetComponent<RoadType>().type == TypeRoad.TurnRoadL || getRoadDict.Road[lanetarget].GetComponent<RoadType>().type == TypeRoad.TurnRoadR)
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
}


