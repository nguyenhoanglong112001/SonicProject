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
    public GameObject pauseUI;
    public float delaytime;

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
    IEnumerator ResumeGame()
    {
        yield return new WaitForSecondsRealtime(delaytime);
        Time.timeScale = 1f;
    }
}
