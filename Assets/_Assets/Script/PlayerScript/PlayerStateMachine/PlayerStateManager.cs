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
    [SerializeField] public CollectManager check;
    public float minspeed;
    public Coroutine crouchCoroutine;
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

    [Header("chi so roll")]
    public float landSpeed;
    public float timeroll;

    [Header("Chi so dash")]
    public float duration;
    public float timeclick;
    public float lastclicktime = -1;
    public int playerlayer;
    public int blockerlayer;

    [Header("bien bool chung")]
    public bool isball;
    public bool isjump;
    public bool isDash;


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
        Dash();
        currentState.UpdateState(this);
        Debug.Log(currentState);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
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
                        if (lane < 1 && CheckChangeLane(lane + 1, lane) /*&& /*!checkcollect.Isenerbeam*/)
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
                    if(!isDash)
                    {
                        if (deltalY > 0 && checkCondition.GroundCheck())
                        {
                            currentState = state.Jump();
                            SwitchState(currentState);
                        }
                        else
                        {
                            currentState = state.Roll();
                            SwitchState(currentState);
                        }
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

    public IEnumerator ChangeCrouch()
    {
        Debug.Log("Roll");
        switchcheck.ChangeBall();
        yield return new WaitForSeconds(timeroll);
        switchcheck.SwitchToCharacter();
        currentState = state.Run();
        SwitchState(currentState);
    }

    public void Crouch()
    {
        crouchCoroutine = StartCoroutine(ChangeCrouch());
    }

    public void MoveForward()
    {
        Vector3 forwardMovement = Vector3.forward * speed * Time.deltaTime;
        playerrigi.MovePosition(playerrigi.position + forwardMovement);
    }

    private void Dash()
    {
        if (check.Energydash == 100)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - lastclicktime < timeclick)
                {
                    currentState = state.Dash();
                    SwitchState(currentState);
                }
                lastclicktime = Time.time;
            }
        }
    }

    public IEnumerator DashTime()
    {
        float energy = check.Energydash;
        float eslapedTime = 0;
        while (eslapedTime < duration)
        {
            eslapedTime += Time.deltaTime;
            check.Energydash = Mathf.Lerp(energy, 0, eslapedTime / duration);
            yield return null;
        }
        isDash = false;
        playeranimator.SetBool("Dash", false);
        Physics.IgnoreLayerCollision(playerlayer, blockerlayer, false);
        SpeedUp(1 / 1.5f);
        currentState = state.Run();
        SwitchState(currentState);
    }

    public void SpeedUp(float mutilpl)
    {
        speed *= mutilpl;
        if (speed < minspeed)
        {
            speed = minspeed;
        }
    }
}



