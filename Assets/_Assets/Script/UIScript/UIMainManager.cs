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
    [SerializeField] private Button backBt;
    [SerializeField] private List<GameObject> teamUI;
    [SerializeField] private List<GameObject> MainUI;
    [SerializeField] private ToggleGroup group;

    private void Start()
    {
        SetGoldUI(SaveManager.instance.GetIntData(SaveKey.CurrentGold, 0));
        SetRedRingUI(SaveManager.instance.GetIntData(SaveKey.CurrentRedRing, 0));
        startBt.onClick.AddListener(StartGame);
        teamBt.onClick.AddListener(OnTeamPress);
        backBt.onClick.AddListener(OnBackPress);
        CurrencyManager.instance.OnUpdateGold.AddListener(SetGoldUI);
        CurrencyManager.instance.OnUpdateRedRing.AddListener(SetRedRingUI);
    }

    private void StartGame()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.btSound, SoundManager.instance.playBtSound);
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
        SoundManager.instance.PlaySound(SoundManager.instance.btSound, SoundManager.instance.popupSound);
        foreach(Toggle toggle in group.GetComponentsInChildren<Toggle>())
        {
            if(toggle.GetComponent<RunnerUI>().type == CharacterType.LeadRunner)
            {
                toggle.isOn = true;
            }
            else
            {
                toggle.isOn = false;
            }
        }
        foreach(GameObject ui in MainUI)
        {
            ui.SetActive(false);
        }
        foreach(GameObject team in teamUI)
        {
            team.SetActive(true);
        }
    }

    private void OnBackPress()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.btSound, SoundManager.instance.backSound);
        foreach (GameObject ui in MainUI)
        {
            ui.SetActive(true);
        }
        foreach (GameObject team in teamUI)
        {
            team.SetActive(false);
        }
    }
}
