using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    private bool isplaying = false;
    [SerializeField] private List<MonoBehaviour> scriptCom;
    [SerializeField] private InputManager PlayerInput;
    [SerializeField] private PlayerControll playerdeath;
    [SerializeField] private DashPower dashcom;

    void Start()
    {
        playerdeath.isalive = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartGame();
        //if (!isplaying)
        //{
        //    return;
        //}
        if(!playerdeath.isalive)
        {
            EndGame();
        }
    }
    private void Awake()
    {
        GetCom();
        Init();
    }

    private void GetCom()
    {
        PlayerInput = GameObject.FindWithTag("Player").GetComponent<InputManager>();
        dashcom = GameObject.FindWithTag("Player").GetComponent<DashPower>();
        playerdeath = GameObject.FindWithTag("Player").GetComponent<PlayerControll>();
    }     

    private void Init()
    {
        GetReferrence();
        //DisableComponents();
    }
    private void GetReferrence()
    {
        scriptCom = new List<MonoBehaviour>();
        scriptCom.Add(PlayerInput);
        scriptCom.Add(dashcom);
    }

    private void StartGame()
    {

    }

    private void EndGame()
    {
        DisableComponents();
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }


    private void DisableComponents()
    {
        SetActiveComponents(false);
    }

    private void EnableComponents()
    {
        SetActiveComponents(true);
    }

    private void SetActiveComponents(bool isActive)
    {
        foreach (var component in scriptCom)
        {
            if (component != null)
            {
                component.enabled = isActive;
            }
        }
    }

    private bool IsPointerOverUIObject(Vector2 position)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    public bool CheckPlaying()
    {
        return isplaying;
    }
}
