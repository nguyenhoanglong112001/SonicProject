using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngameManager : MonoBehaviour
{
    public static UIIngameManager instance;
    public Button pauseBt;
    public Button quitBt;
    public Button SettingBt;
    public Button ResumeBt;
    public Button exitBt;
    public Button ReviveBt;
    public GameObject pauseUI;
    public GameObject endUI;
    public float delaytime;
    public Text currentRedRing;

    public GameObject player;
    public PlayerControll playerControll;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        pauseBt.onClick.AddListener(OnPauseBtPress);
        ResumeBt.onClick.AddListener(OnResumeBtPress);
        quitBt.onClick.AddListener(OnQuitBtPress);
        exitBt.onClick.AddListener(ExitEndGame);
        ReviveBt.onClick.AddListener(OnRevivePress);
    }

    private void OnPauseBtPress()
    {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    private void OnResumeBtPress()
    {
        pauseUI.SetActive(false);
        StartCoroutine(ResumeGame());
    }

    private void OnQuitBtPress()
    {
        GameManager.instance.ChangeGameState(GameState.Menu);
    }

    private void ExitEndGame()
    {
        endUI.SetActive(false);
        GameManager.instance.ChangeGameState(GameState.Menu);
    }
    IEnumerator ResumeGame()
    {
        yield return new WaitForSecondsRealtime(delaytime);
        Time.timeScale = 1f;
    }

    private void OnRevivePress()
    {
        int reviveCost = CurrencyManager.instance.cost.BaseReviveCost * (int)Mathf.Pow(2, GameManager.instance.reviveCount);
        if (CurrencyManager.instance.currentRedRing > reviveCost)
        {
            playerControll.playerRevive();
            reviveCost += 1;
            CurrencyManager.instance.UpdateRedRing(-reviveCost);
            GameManager.instance.ChangeGameState(GameState.InGame);
        }
    }
}
