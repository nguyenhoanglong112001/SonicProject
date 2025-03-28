using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour
{
    public static UpdateUI instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private List<Text> typeTxt;
    [SerializeField] private List<Text> levelTxt;
    [SerializeField] private List<Text> numTxt;
    [SerializeField] private List<Image> updateImage;
    [SerializeField] private CharacterTable characterList;
    [SerializeField] private UpdateTable updateTable;
    [SerializeField] private ToggleGroup characterGroup;

    [SerializeField] private Text characterName;
    [SerializeField] private Image characterImage;

    public Button informationBt;
    [SerializeField] private Button exitBt;
    [SerializeField] private GameObject updateUI;

    [SerializeField] private Sprite selectLv;
    [SerializeField] private Sprite unselectlv;

    [SerializeField] private Button UpdateBt;
    [SerializeField] private Text Updatetxt;

    [Header("===Balance and Cost")]
    public Text balanceTxt;
    public Image balanceImage;
    public Text costTxt;
    public Image costImage;
    public Sprite goldSprite;
    public Sprite redRingSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        informationBt.onClick.AddListener(ShowUpdateUI);
        informationBt.onClick.AddListener(ShowCostBalance);
        exitBt.onClick.AddListener(CloseUpdateUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUpdateUI()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.btSound, SoundManager.instance.popupSound);
        UpdateBt.onClick.AddListener(OnUpdatePress);
        updateUI.SetActive(true);
        Updatetxt.text = "UPGRADE";
        for(int i = 0; i <levelTxt.Count; i++)
        {
            levelTxt[i].text = "Lv " + updateTable.updateList[i].level.ToString();
            numTxt[i].text = "+" + updateTable.updateList[i].bonus.ToString() +"%";
        }
        foreach(Toggle character in characterGroup.GetComponentsInChildren<Toggle>())
        {
            if(character.isOn)
            {
                RunnerUI runner = character.gameObject.GetComponent<RunnerUI>();
                characterName.text = runner.currentCharacter.characterName;
                characterImage.sprite = runner.currentCharacter.CharacterImage2;
                foreach(Text txt in typeTxt)
                {
                    txt.text = runner.currentCharacter.bonusType.ToString();
                }
                for(int i =0;i< updateImage.Count;i++)
                {
                    if(runner.currentLvUpdate == i+1)
                    {
                        updateImage[i].sprite = selectLv;
                    }
                    else
                    {
                        updateImage[i].sprite = unselectlv;
                    }
                }
            }
        }
    }
    public void ShowBuyUI(int characterid)
    {
        UpdateBt.onClick.AddListener(() => OnBuyBtPress(characterid));
        updateUI.SetActive(true);
        Updatetxt.text = "BUY";
        for (int i = 0; i < levelTxt.Count; i++)
        {
            levelTxt[i].text = "Lv " + updateTable.updateList[i].level.ToString();
            numTxt[i].text = "+" + updateTable.updateList[i].bonus.ToString() + "%";
        }
        foreach(Character character in characterList.CharacterList)
        {
            if(character.id == characterid)
            {
                characterName.text = character.characterName;
                characterImage.sprite = character.CharacterImage2;
                balanceImage.sprite = costImage.sprite = redRingSprite;
                balanceTxt.text = CurrencyManager.instance.currentRedRing.ToString();
                costTxt.text = character.Cost.ToString();
                foreach (Text txt in typeTxt)
                {
                    txt.text = character.bonusType.ToString();
                }
                for (int i = 0; i < updateImage.Count; i++)
                {
                    if (i == 0)
                    {
                        updateImage[i].sprite = selectLv;
                    }
                    else
                    {
                        updateImage[i].sprite = unselectlv;
                    }
                }
            }
        }

    }    
    public void CloseUpdateUI()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.btSound, SoundManager.instance.backSound);
        updateUI.SetActive(false);
        UpdateBt.onClick.RemoveAllListeners();
    }

    public void ShowCostBalance()
    {
        foreach(Toggle character in characterGroup.GetComponentsInChildren<Toggle>())
        {
            if(character.isOn)
            {
                RunnerUI runner = character.gameObject.GetComponent<RunnerUI>();
                if(runner.isUnlock)
                {
                    foreach (UpdateInfo updatelv in updateTable.updateList)
                    {
                        if (updatelv.level == runner.currentLvUpdate + 1)
                        {
                            if (updatelv.costType == CostCurrency.Gold)
                            {
                                balanceImage.sprite = costImage.sprite = goldSprite;
                                balanceTxt.text = CurrencyManager.instance.currentGold.ToString();
                                costTxt.text = updatelv.cost.ToString();
                            }
                            else if (updatelv.costType == CostCurrency.RedRing)
                            {
                                balanceImage.sprite = costImage.sprite = redRingSprite;
                                balanceTxt.text = CurrencyManager.instance.currentRedRing.ToString();
                                costTxt.text = updatelv.cost.ToString();
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnUpdatePress()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.btSound, SoundManager.instance.forwardSound);
        foreach (Toggle character in characterGroup.GetComponentsInChildren<Toggle>())
        {
            if(character.isOn)
            {
                RunnerUI runner = character.gameObject.GetComponent<RunnerUI>();
                if(runner.isUnlock)
                {
                    foreach (UpdateInfo update in updateTable.updateList)
                    {
                        if (runner.currentLvUpdate + 1 == update.level)
                        {
                            if (update.costType == CostCurrency.Gold)
                            {
                                if (CurrencyManager.instance.currentGold >= update.cost)
                                {
                                    UpdateCharacter(CostCurrency.Gold,runner,update);
                                    break;
                                }
                            }
                            else if (update.costType == CostCurrency.RedRing)
                            {
                                if (CurrencyManager.instance.currentRedRing >= update.cost)
                                {
                                    UpdateCharacter(CostCurrency.RedRing,runner,update);
                                    break;
                                }
                            }
                        }
                    }
                }  
            }
        }    
    }

    private void UpdateCharacter(CostCurrency costType,RunnerUI runner, UpdateInfo update)
    {
        runner.currentCharacter.currentlevel += 1;
        runner.currentLvUpdate += 1;
        runner.levelText.text = "Lv. " + runner.currentCharacter.currentlevel.ToString();
        SaveManager.instance.Save(SaveKey.characterLv[runner.currentID], runner.currentLvUpdate);
        if(costType == CostCurrency.Gold)
        {
            CurrencyManager.instance.UpdateGoldRing(-update.cost);
        }
        else
        {
            CurrencyManager.instance.UpdateRedRing(-update.cost);
        }
        ShowCostBalance();
        for (int i = 0; i < updateImage.Count; i++)
        {
            if (runner.currentLvUpdate == i + 1)
            {
                updateImage[i].sprite = selectLv;
            }
            else
            {
                updateImage[i].sprite = unselectlv;
            }
        }
    }

    private void OnBuyBtPress(int id)
    {
        SoundManager.instance.PlaySound(SoundManager.instance.btSound, SoundManager.instance.forwardSound);
        foreach (Character character in characterList.CharacterList)
        {
            if(id == character.id && !character.IsUnlock)
            {
               if (CurrencyManager.instance.currentRedRing >= character.Cost)
                {
                    character.IsUnlock = true;
                    updateUI.SetActive(false);
                    ShopManager.Instance.OnBuySucced(id);
                    CharacterUI.instance.characterCost.SetActive(false);
                }
            }
        }
    }
}
