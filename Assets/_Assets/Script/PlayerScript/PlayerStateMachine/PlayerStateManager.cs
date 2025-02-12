using DG.Tweening;
using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;
    public PlayerBaseState newState;
    public PlayerStateFactory state;

    [Header("Chi so chung")]
    [SerializeField] public PlayerControll checkCondition;
    [SerializeField] public Animator playeranimator;
    [SerializeField] public SwitchBall switchcheck;
    [SerializeField] public CollectManager check;
    public CapsuleCollider playerCollider;
    public TypeRoad laneType;
    public float minspeed;
    public Rigidbody playerrigi;
    public SplineFollower follower;
    [Header("Chi so chay")]
    public float speed;
    private Vector3 startMousepoint;
    private Vector3 endMousepoint;
    public float distancetouch;
    public int lane = 1; //1 = mid ; 0 = left ; 2 = right;
    public float lanedistance;
    public CheckLane getRoadDict;
    public Vector3 moveDirection;

    [Header("Chi so nhay")]
    public float jumpforce;

    [Header("chi so roll")]
    public float landSpeed;
    public float timeroll;
    public Coroutine crouchCoroutine;

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
    public bool israil;
    public bool isTurn;

    [Header("Dotween Rail")]
    public Transform[] path;
    public Tween currentTween;
    public float moveduration;
    public Vector3 newpos;
    private Dictionary<float, GameObject> rail;
    public Transform[] currentPath;
    public float enerbeamDuration;

    [Header("Enerbeam State")]
    public GameObject enerbeamPrefab;
    public Transform pos;
    public Transform grappoint;
    public GameObject ener;
    private GameObject splinepos;
    public GameObject enerbeamRail;
    public Vector3 startPos;
    public float speedToRail;

    [Header("Fall State")]
    public float fallspeed;

    [Header("Turn State")]
    public Vector3 posAfterTurn;
    public float progress;
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
        if(currentState != null)
        {
            currentState.UpdateState(this);
        }
        Debug.Log("State: " + currentState);
    }
    private void FixedUpdate()
    {
        MoveForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartRail"))
        {
            israil = true;
            path = other.gameObject.GetComponentInParent<WayPointList>().wayPoints;
            currentPath = path;
            GameObject objparent = other.transform.parent.transform.parent.transform.parent.gameObject;
            rail = objparent.GetComponent<CheckLane>().Road;
            if (currentPath != null)
            {
                if (Vector3.Distance(currentPath[0].position, transform.position) > 0.1f)
                {
                    currentPath[0] = transform;
                }
                if (currentState is not GrindState)
                {
                    newState = state.Grind();
                    SwitchState(newState);
                }
                else
                {
                    MoveWayPoint();
                }
            }
        }
        if (currentState is GrindState)
        {
            if (other.CompareTag("EndRail"))
            {
                currentPath = null;
            }
        }
        if(other.CompareTag("StartTurn"))
        {
            follower.spline = other.GetComponent<SplineComputer>();
            newState = state.Turn();
            SwitchState(newState);
        }
        if (other.CompareTag("EnerbeamPickup"))
        {
            splinepos = other.gameObject.transform.GetChild(0).gameObject;
            playerrigi.linearVelocity = Vector3.zero;
            //newState = state.Enerbeam();
            //SwitchState(newState);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameObject objParent = collision.gameObject.transform.parent.gameObject;
            getRoadDict = objParent.GetComponentInParent<CheckLane>();
            laneType = collision.gameObject.GetComponentInParent<Road>().type;
        }
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState.ExitState(this);
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
                        if (lane < 2 && CheckChangeLane(lane + 1, lane) /*&& /*!checkcollect.Isenerbeam*/)
                        {
                            //if (checkCondition._canDodge)
                            //{
                            //    checkCondition.ComboUpdate("Dodge");

                            //}
                            ChangeLane(1);
                            //lane++;
                        }
                    }
                    else
                    {
                        if (lane > 0 && CheckChangeLane(lane - 1, lane) /*&& !checkcollect.Isenerbeam*/)
                        {
                            //if (checkCondition._canDodge)
                            //{
                            //    checkCondition.ComboUpdate("Dodge");
                            //}
                            ChangeLane(-1);
                            //lane--;
                        }
                    }
                    //Vector3 targetPosition = transform.position;
                    //targetPosition.x = lane * lanedistance;
                    //transform.position = targetPosition;
                }
                else
                {
                    if(!isDash)
                    {
                        if (deltalY > 0 && checkCondition.GroundCheck())
                        {
                            newState = state.Jump();
                            SwitchState(newState);
                        }
                        else
                        {
                            newState = state.Roll();
                            SwitchState(newState);
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

    public IEnumerator ChangeCrouch()
    {
        Debug.Log("Roll");
        switchcheck.ChangeBall();
        yield return new WaitForSeconds(timeroll);
        switchcheck.SwitchToCharacter();
        newState = state.Run();
        SwitchState(newState);
    }

    public void Crouch()
    {
        crouchCoroutine = StartCoroutine(ChangeCrouch());
    }

    public void MoveForward()
    {
        moveDirection = transform.forward;
        Vector3 forwardMovement = moveDirection * speed * Time.fixedDeltaTime;
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
                    newState = state.Dash();
                    SwitchState(newState);
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
        newState = state.Run();
        SwitchState(newState);
    }

    public void SpeedUp(float mutilpl)
    {
        speed *= mutilpl;
        if (speed < minspeed)
        {
            speed = minspeed;
        }
    }

    public void MoveWayPoint()
    {
        if (currentPath != null)
        {
            Vector3[] pointPath = System.Array.ConvertAll(currentPath, t => t.position);
            if (pointPath[0] == transform.position)
            {
                List<Vector3> tempPath = new List<Vector3>(pointPath);
                tempPath.RemoveAt(0);
                pointPath = tempPath.ToArray();
            }
            currentTween = transform.DOPath(pointPath, moveduration, PathType.CatmullRom, PathMode.Full3D)
                .SetEase(Ease.Linear)
                .SetLookAt(0.01f)
                .OnComplete(() =>
            {
                if (currentPath == null && currentState is GrindState)
                {
                    Debug.Log("Complete");
                    playerrigi.isKinematic = false;
                    pointPath = null;
                    newState = state.Run();
                    SwitchState(newState);
                }
            });
        }
    }


    public void Turn()
    {
        follower.follow = true;
        follower.SetPercent(0);
        follower.followSpeed = speed;
        follower.onEndReached += OnCompletedSpline;
    }

    public void OnCompletedSpline(double value)
    {
        follower.spline = null;
        follower.follow = false;
        newState = state.Run();
        SwitchState(newState);
    }

    public GameObject SpawnEnerbeam()
    {
        ener = Instantiate(enerbeamPrefab, pos.position, enerbeamPrefab.transform.rotation);
        grappoint = ener.transform.GetChild(0).transform.GetChild(4);
        playerrigi.useGravity = false;
        gameObject.transform.SetParent(ener.transform);
        return ener;
    }

    public void DestroyEnerbeam()
    {
        playerrigi.useGravity = true;
        Destroy(ener);
    }

    private void ChangeLane(int direction)
    {
        int targetLane = Mathf.Clamp(lane + direction, 0, 2);
        if(targetLane != lane)
        {
            lane = targetLane;
            Vector3 targetPos = transform.position + transform.right * direction * lanedistance;
            StartCoroutine(MoveToLane(targetPos));
        }
    }

    IEnumerator MoveToLane(Vector3 targetPos)
    {
        float elapsedTime = 0f;
        float moveDuration = 0.2f;

        Vector3 startPos = transform.position;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }

}



