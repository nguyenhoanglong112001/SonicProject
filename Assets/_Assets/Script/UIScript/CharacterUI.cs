using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public List<Toggle> RunnerBt;
    public ToggleGroup toggleGroup;
    public CharacterTable CharacterInfo;
    public Text characterTxt;
    public Text BonusTxt;
    public Image imageCharacter;
    public GameObject EmptyBt;

    public Sprite selectImage;
    public Sprite unSelectImage;

    public Dictionary<Toggle, int> idBtToggle; 

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
            if(character.id == id)
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
                            }
                        }
                        else
                        {
                            if (idBtToggle.Values.Contains(id))
                            {
                                Toggle key = idBtToggle.FirstOrDefault(kvp => kvp.Value == id).Key;
                                idBtToggle[key] = toggle.GetComponent<RunnerUI>().currentID;
                                key.GetComponent<RunnerUI>().currentID = idBtToggle[key];
                                key.GetComponent<RunnerUI>().GetImage(idBtToggle[key]);
                                toggle.GetComponent<RunnerUI>().currentID = id;
                                idBtToggle[toggle] = id;
                                SetChracterChoice(character, id);
                            }
                            else
                            {
                                toggle.GetComponent<RunnerUI>().currentID = id;
                                idBtToggle[toggle] = id;
                                SetChracterChoice(character, id);
                            }    
                        }
                    }
                }
            }
        }
    }

    private void SetChracterChoice(Character character,int id)
    {
        characterTxt.text = character.characterName;
        imageCharacter.sprite = character.CharacterImage;
        BonusTxt.text = character.bonusType.ToString();
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
                imageCharacter = runnerUI.characterImage;
                EmptyBt = runnerUI.EmptySlot;
                foreach(Character character in CharacterInfo.CharacterList)
                {
                    if(runnerUI.currentID == character.id)
                    {
                        characterTxt.text = character.characterName;
                        BonusTxt.text = character.bonusType.ToString();
                    }
                }
            }
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
}
