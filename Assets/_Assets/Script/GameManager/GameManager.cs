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
    public GameState currentState;
    [SerializeField] public PlayerControll playerControll;
    [SerializeField] public PlayerStateManager playerState;
    public int reviveCount;

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
        playerControll = PlayerManager.instance.playerControll;
        playerState = PlayerManager.instance.playerState;
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
            UIIngameManager.instance.currentRedRing.text = SaveManager.instance.GetIntData(SaveKey.CurrentRedRing, 0).ToString();
        }
        if(state == GameState.InGame)
        {
            if(currentState != GameState.EndGame)
            {
                SceneManager.LoadScene(1);
            }
        }
        if(state == GameState.Menu)
        {
            SceneManager.LoadScene(0);
            reviveCount = 0;
            if (currentState == GameState.EndGame)
            {
                CurrencyManager.instance.UpdateGoldRing(SaveManager.instance.GetIntData(SaveKey.GoldRingBank, 0));
                CurrencyManager.instance.UpdateRedRing(SaveManager.instance.GetIntData(SaveKey.RedRing, 0));
                SaveManager.instance.Save(SaveKey.GoldRingBank, 0);
                SaveManager.instance.Save(SaveKey.RedRing, 0);
            }
        }
        currentState = state;
    }

    private void OnSceneLoaded(Scene scene,LoadSceneMode mode)
    {
        if(scene.name == "PlayScene")
        {
            GetCom();
            PlayerManager.instance.isAlive = true;
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
