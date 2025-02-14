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

    private void Start()
    {
        SetGoldUI(SaveManager.instance.GetIntData(SaveKey.CurrentGold, 0));
        SetRedRingUI(SaveManager.instance.GetIntData(SaveKey.CurrentRedRing, 0));
        startBt.onClick.AddListener(StartGame);
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
}
