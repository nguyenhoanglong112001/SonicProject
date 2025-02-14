using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    InGame,
    EndGame
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameState currentState;
    [SerializeField] private PlayerControll playerControll;
    [SerializeField] private PlayerStateManager playerState;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void GetCom()
    {
        playerControll = GameObject.FindWithTag("Player").GetComponent<PlayerControll>();
        playerState = GameObject.FindWithTag("Player").GetComponent<PlayerStateManager>();
    }     

    private bool IsPointerOverUIObject(Vector2 position)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void ChangeGameState(GameState state)
    {
        if(state == GameState.EndGame)
        {
            playerState.currentState = null;
            UIIngameManager.instance.endUI.SetActive(true);
        }
        if(state == GameState.InGame)
        {
            SceneManager.LoadScene(1);
        }
        if(state == GameState.Menu)
        {
            SceneManager.LoadScene(0);
        }
        currentState = state;
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if(scene.name == "PlayScene")
        {
            GetCom();
            playerControll.isalive = true;
        }
        else if (scene.name == "Start")
        {
            playerControll = null;
            playerState = null;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
