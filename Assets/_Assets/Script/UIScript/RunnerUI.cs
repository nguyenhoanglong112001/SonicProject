using UnityEngine;
using UnityEngine.UI;

public enum CharacterType
{
    LeadRunner,
    SideRunner1,
    SideRunner2,
}
public class RunnerUI : MonoBehaviour
{
    public int currentID;
    public int currentLvUpdate;
    public bool isUnlock;
    public CharacterType type;
    public CharacterTable characterTable;
    public Character currentCharacter;
    public GameObject EmptySlot;
    public Image characterImage;
    public Text levelText;

    private void Start()
    {

    }

    public void SetCharacter(int id)
    {
        foreach(Character character in characterTable.CharacterList)
        {
            if(character.id == id)
            {
                characterImage.sprite = character.CharacterImage;
                currentCharacter = character;
                currentLvUpdate = character.currentlevel;
                isUnlock = character.IsUnlock;
            }
        }
    }

    public void SetStart()
    {
        if (type == CharacterType.LeadRunner)
        {
            currentID = SaveManager.instance.GetIntData(SaveKey.LeadRunner, 0);
            if(currentID == 0)
            {
                currentID = 1;
                SaveManager.instance.Save(SaveKey.LeadRunner, 1);
            }
        }
        else if (type == CharacterType.SideRunner1)
        {
            currentID = SaveManager.instance.GetIntData(SaveKey.SideRunner1, 0);
        }
        else if (type == CharacterType.SideRunner2)
        {
            currentID = SaveManager.instance.GetIntData(SaveKey.SideRunner2, 0);
        }
        SetCharacter(currentID);
        levelText.text = "Level" + currentLvUpdate.ToString();
        if (currentID == 0)
        {
            EmptySlot.SetActive(true);
            characterImage.gameObject.SetActive(false);
        }
        else
        {
            EmptySlot.SetActive(false);
            characterImage.gameObject.SetActive(true);
        }
    }
}
