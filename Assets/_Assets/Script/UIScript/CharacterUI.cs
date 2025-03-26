using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public static CharacterUI instance;
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
    public List<Toggle> RunnerBt;
    public ToggleGroup toggleGroup;
    public CharacterTable CharacterInfo;
    public UpdateTable updateList;
    public Text characterTxt;
    public Text BonusTxt;
    public Text LevelText;
    public Image imageCharacter;
    public GameObject EmptyBt;
    public Text currentBonus;
    public Text nextUpdateBonus;

    public Sprite selectImage;
    public Sprite unSelectImage;

    public Dictionary<Toggle, int> idBtToggle;

    public GameObject updateUI;
    public GameObject characterCost;
    public GameObject characterInfo;


    private void Start()
    {
        foreach(Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.onValueChanged.AddListener((ison) => OnToggleChoice());
            toggle.GetComponent<RunnerUI>().SetStart();
        }

        idBtToggle = new Dictionary<Toggle, int>()
        {
            {RunnerBt[0],SaveManager.instance.GetIntData(SaveKey.SideRunner2,0) },
            {RunnerBt[1],SaveManager.instance.GetIntData(SaveKey.SideRunner1,0) },
            {RunnerBt[2],SaveManager.instance.GetIntData (SaveKey.LeadRunner,0) },
        };
    }

    public void ChoiceCharacter(int id)
    {
        foreach(Character character in CharacterInfo.CharacterList)
        {
            if(character.id == id && character.IsUnlock)
            {
                foreach(Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
                {
                    if(toggle.isOn)
                    {
                        if (toggle.GetComponent<RunnerUI>().currentID == 0)
                        {
                            if (idBtToggle.Values.Contains(id))
                            {
                                return;
                            }
                            else
                            {
                                toggle.GetComponent<RunnerUI>().currentID = id;
                                idBtToggle[toggle] = id;
                                SetChracterChoice(character, id);
                                toggle.GetComponent<RunnerUI>().currentCharacter = character;
                            }
                        }
                        else
                        {
                            if (idBtToggle.Values.Contains(id))
                            {
                                Toggle key = idBtToggle.FirstOrDefault(kvp => kvp.Value == id).Key;
                                idBtToggle[key] = toggle.GetComponent<RunnerUI>().currentID;
                                key.GetComponent<RunnerUI>().currentID = idBtToggle[key];
                                key.GetComponent<RunnerUI>().SetCharacter(idBtToggle[key]);
                                toggle.GetComponent<RunnerUI>().currentID = id;
                                idBtToggle[toggle] = id;
                                SetChracterChoice(character, id);
                                toggle.GetComponent<RunnerUI>().currentCharacter = character;
                            }
                            else
                            {
                                toggle.GetComponent<RunnerUI>().currentID = id;
                                idBtToggle[toggle] = id;
                                SetChracterChoice(character, id);
                                toggle.GetComponent<RunnerUI>().currentCharacter = character;
                            }    
                        }
                        SetCharacterInfoUI(true, Color.green);
                    }
                }
            }
            else if (character.IsUnlock == false && character.id == id)
            {
                UpdateUI.instance.ShowBuyUI(character.id);
            }
        }
    }

    private void SetChracterChoice(Character character,int id)
    {
        characterTxt.text = character.characterName;
        imageCharacter.sprite = character.CharacterImage;
        BonusTxt.text = character.bonusType.ToString();
        LevelText.text = "Lv. " + character.currentlevel.ToString();
        imageCharacter.gameObject.SetActive(true);
        EmptyBt.SetActive(false);
        SaveChoice(id);
    }

    public void OnToggleChoice()
    {
        foreach(Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            Image toggleImage = toggle.GetComponent<Image>();
            RunnerUI runnerUI = toggle.GetComponent<RunnerUI>();
            toggleImage.sprite = toggle.isOn ? selectImage : unSelectImage;
            if(toggle.isOn)
            {
                if(runnerUI.currentID > 0)
                {
                    SetCharacterInfoUI(true, Color.green);
                }
                else
                {
                    SetCharacterInfoUI(false, Color.gray);
                }
                imageCharacter = runnerUI.characterImage;
                LevelText = runnerUI.levelText;
                EmptyBt = runnerUI.EmptySlot;
                foreach(Character character in CharacterInfo.CharacterList)
                {
                    if(runnerUI.currentID == character.id)
                    {
                        characterTxt.text = character.characterName;
                        BonusTxt.text = character.bonusType.ToString();
                        foreach (UpdateInfo update in updateList.updateList)
                        {
                            if (character.currentlevel == update.level)
                            {
                                currentBonus.text = update.bonus.ToString() + "%";
                                if(character.currentlevel < 15)
                                {
                                    nextUpdateBonus.text = updateList.updateList[character.currentlevel].bonus.ToString() + "%";
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void SetCharacterInfoUI(bool isActive,Color color)
    {
        characterInfo.GetComponent<Image>().color = color;
        BonusTxt.gameObject.SetActive(isActive);
        currentBonus.gameObject.SetActive(isActive);
        nextUpdateBonus.gameObject.SetActive(false);
        UpdateUI.instance.informationBt.interactable = isActive;
        foreach (Transform child in characterInfo.transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }

    public void SaveChoice(int id)
    {
        foreach (Toggle toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            if (toggle.isOn)
            {
                RunnerUI runnerUI = toggle.GetComponent<RunnerUI>();
                if (runnerUI.type == CharacterType.LeadRunner)
                {
                    CharacterManager.instance.SetIdChoice(id);
                    SaveManager.instance.Save(SaveKey.LeadRunner, id);
                }
                else
                {
                    if (runnerUI.type == CharacterType.SideRunner1)
                    {
                        SaveManager.instance.Save(SaveKey.SideRunner1, id);
                    }
                    else if (runnerUI.type == CharacterType.SideRunner2)
                    {
                        SaveManager.instance.Save(SaveKey.SideRunner2, id);
                    }
                }
            }
        }
    }

    public void SetUpForBuy(GameObject costObj)
    {
        characterCost = costObj;
    }
}
