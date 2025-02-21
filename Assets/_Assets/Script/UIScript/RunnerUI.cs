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
    public CharacterType type;
    public CharacterTable characterTable;
    public GameObject EmptySlot;
    public Image characterImage;

    private void Start()
    {

    }

    public void GetImage(int id)
    {
        foreach(Character character in characterTable.CharacterList)
        {
            if(character.id == id)
            {
                characterImage.sprite = character.CharacterImage;
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
        GetImage(currentID);
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
