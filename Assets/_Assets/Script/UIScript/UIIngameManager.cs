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

    [Header("ComboUI")]
    [SerializeField] private Text comboText;
    [SerializeField] private Text comboTypeText;
    [SerializeField] private Image comboCountDown;
    [SerializeField] private GameObject comboPannel;


    public GameObject player;

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

        ComboManager.instance.OnComboChange.AddListener(ShowCombo);
        comboPannel.SetActive(false);
    }

    private void Update()
    {
        CountDownCombo();
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
            endUI.SetActive(false);
            PlayerManager.instance.playerControll.PlayerRevive();
            reviveCost += 1;
            CurrencyManager.instance.UpdateRedRing(-reviveCost);
            GameManager.instance.ChangeGameState(GameState.InGame);
        }
    }

    public void ShowCombo()
    {
        gameObject.SetActive(true);
        comboText.text = ComboManager.instance.ComboCount.ToString();
    }

    public void ShowCombotype(string comboType)
    {
        comboTypeText.text = comboType + "!";
    }

    public void CountDownCombo()
    {
        comboCountDown.fillAmount = ComboManager.instance.RemainTime / ComboManager.instance.ComboTime;
    }
}
