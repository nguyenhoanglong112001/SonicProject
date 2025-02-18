using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public CharacterTable characterTable;
    public int idChoose;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void SetIdChoice(int idChoice)
    {
        idChoose = idChoice;
    }

    public void SetCharacter(Transform pos,Camera mainCam,Camera enerCam)
    {
        foreach(var idCharacter in characterTable.CharacterList)
        {
            if(idCharacter.id == idChoose)
            {
                GameObject Character = Instantiate(idCharacter.CharacterPrefab, pos.position, Quaternion.identity);
                ScoreManager.instance.player = Character;
                mainCam.transform.SetParent(Character.transform);
                enerCam.transform.SetParent(Character.transform);
                PlayerManager.instance.playerControll = Character.GetComponent<PlayerControll>();
                PlayerManager.instance.playerState = Character.GetComponent<PlayerStateManager>();
                GameManager.instance.playerControll = PlayerManager.instance.playerControll;
                GameManager.instance.playerState = PlayerManager.instance.playerState;
            }
        }
    }
}
