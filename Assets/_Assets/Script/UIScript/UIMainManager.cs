using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainManager : MonoBehaviour
{
    [SerializeField] private Button startBt;
    [SerializeField] private Text goldTxt;
    [SerializeField] private Text redRingTxt;
    [SerializeField] private Button teamBt;
    [SerializeField] private List<GameObject> teamUI;
    [SerializeField] private List<GameObject> MainUI;

    private void Start()
    {
        SetGoldUI(SaveManager.instance.GetIntData(SaveKey.CurrentGold, 0));
        SetRedRingUI(SaveManager.instance.GetIntData(SaveKey.CurrentRedRing, 0));
        startBt.onClick.AddListener(StartGame);
        teamBt.onClick.AddListener(OnTeamPress);
        CurrencyManager.instance.OnUpdateGold.AddListener(SetGoldUI);
        CurrencyManager.instance.OnUpdateRedRing.AddListener(SetRedRingUI);
    }

    private void StartGame()
    {
        GameManager.instance.ChangeGameState(GameState.InGame);
    }

    private void SetGoldUI(int currentGold)
    {
        goldTxt.text = currentGold.ToString();
    }

    private void SetRedRingUI(int currentRedRing)
    {
        redRingTxt.text = currentRedRing.ToString();
    }

    private void OnTeamPress()
    {
        foreach(GameObject ui in MainUI)
        {
            ui.SetActive(false);
        }
        foreach(GameObject team in teamUI)
        {
            team.SetActive(true);
        }
    }
}
