﻿using DG.Tweening;
using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;
    public PlayerBaseState newState;
    public PlayerStateFactory state;

    [Header("Chi so chung")]
    public PlayerControll playControll;
    public Animator playeranimator;
    public SwitchBall switchcheck;
    public CharacterVoice voice;
    public Collider playerCollider;
    public TypeRoad laneType;
    public float minspeed;
    public Rigidbody playerrigi;
    public SplineFollower follower;
    public AudioSource playerSound;
    public AudioSource dashSound;
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

    [Header("VFX")]
    public GameObject moveLeftVFX;
    public GameObject moveRightVFX;
    public GameObject DashVFX;
    public GameObject dashEndVFX;
    public GameObject slamVFX;
    public GameObject ShieldVFX;
    public GameObject ShieldendVFX;
    private void Awake()
    {
        state = new PlayerStateFactory(this);
        currentState = state.Run();
        currentState.EnterState(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        voice = GetComponentInChildren<CharacterVoice>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.timeScale);
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
        if (PlayerManager.instance.isAlive)
        {
            MoveForward();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartRail") && follower.spline == null)
        {
            Debug.Log("Trigger");
            follower.spline = other.GetComponent<SplineComputer>();
            newState = state.Grind();
            SwitchState(newState);
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
                            moveRightVFX.SetActive(true);
                            moveRightVFX.GetComponent<ParticleSystem>().Play();
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
                            moveLeftVFX.SetActive(true);
                            moveLeftVFX.GetComponent<ParticleSystem>().Play();
                            //lane--;
                        }
                    }
                    //Vector3 targetPosition = transform.position;
                    //targetPosition.x = lane * lanedistance;
                    //transform.position = targetPosition;
                }
                else
                {
                    if(!isDash && currentState is not TurnState)
                    {
                        if (deltalY > 0 && playControll.GroundCheck())
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
        switchcheck.ChangeBall();
        SoundManager.instance.PlaySound(playerSound, SoundManager.instance.rollSound);
        yield return new WaitForSeconds(timeroll);
        SoundManager.instance.StopSound(playerSound);
        switchcheck.SwitchToCharacter();
        newState = state.Run();
        SwitchState(newState);
    }

    IEnumerator ActiveSlamVFX()
    {
        yield return new WaitUntil(() => playControll.GroundCheck());
        slamVFX.SetActive(true);
        ParticleSystem ps =slamVFX.GetComponent<ParticleSystem>();
        ps.Play();

        yield return new WaitUntil(() => ps.IsAlive() == false);
        slamVFX.SetActive(false);
    }

    public void TurnOnSlanVFX()
    {
        StartCoroutine(ActiveSlamVFX());
    }

    public IEnumerator WaitToChangeBall()
    {
        AnimatorStateInfo stateinfo = playeranimator.GetCurrentAnimatorStateInfo(0);

        while (stateinfo.IsName("sonic_roll_in") && stateinfo.normalizedTime < 1.0f)
        {
            yield return null;
            stateinfo = playeranimator.GetCurrentAnimatorStateInfo(0);
        }
        switchcheck.ChangeBall();
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
        if (CollectManager.instance.Energydash == 100)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time - lastclicktime < timeclick)
                {
                    SoundManager.instance.PlaySound(dashSound, SoundManager.instance.startDashSound);
                    newState = state.Dash();
                    SwitchState(newState);
                }
                lastclicktime = Time.time;
            }
        }
    }

    public IEnumerator DashTime()
    {
        float energy = CollectManager.instance.Energydash;
        float eslapedTime = 0;
        while (eslapedTime < duration)
        {
            eslapedTime += Time.deltaTime;
            CollectManager.instance.Energydash = Mathf.Lerp(energy, 0, eslapedTime / duration);
            if(CollectManager.instance.Energydash <= 20)
            {
                foreach (Transform vfx in DashVFX.transform)
                {
                    if(vfx.gameObject != dashEndVFX)
                    {
                        vfx.gameObject.SetActive(false);
                    }
                }
            }
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

    public void OnCompletRail(double value)
    {
        SplineComputer nextSpline = CheckNextSpline();
        follower.onEndReached -= OnCompletRail;
        if (nextSpline == null)
        {
            playerrigi.isKinematic = false;
            follower.follow = false;
            follower.spline = null;
            newState = state.Run();
            SwitchState(newState);
        }
        else
        {
            follower.spline = nextSpline;
            EnterSpline(OnCompletRail);
        }
    }

    private SplineComputer CheckNextSpline()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 1.0f);

        foreach(Collider hit in hits)
        {
            SplineComputer spline = hit.GetComponent<SplineComputer>();
            return spline;
        }
        return null;
    }    


    public void EnterSpline(Action<double> Oncomplete)
    {
        follower.follow = true;
        follower.SetPercent(0,true);
        follower.followSpeed = speed;
        follower.onEndReached -= Oncomplete;
        follower.onEndReached += Oncomplete;
    }

    public void OnCompletedSpline(double value)
    {
        follower.spline = null;
        follower.follow = false;
        newState = state.Run();
        SwitchState(newState);
        follower.onEndReached -= OnCompletedSpline;
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



