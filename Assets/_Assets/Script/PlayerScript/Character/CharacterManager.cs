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

    public void SetCharacter(Transform pos, PlayerStateManager player, PlayerControll playerControll)
    {
        foreach(var idCharacter in characterTable.CharacterList)
        {
            if(idCharacter.id == idChoose)
            {
                GameObject Character = Instantiate(idCharacter.CharacterPrefab, pos.position, Quaternion.identity, pos);
                player.playeranimator = Character.GetComponent<Animator>();
                playerControll.playeranimator = Character.GetComponent<Animator>();
  
            }
        }
    }
}
