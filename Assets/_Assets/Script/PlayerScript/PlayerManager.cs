using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public bool isAlive;

    public Transform pos;

    [SerializeField] private Camera MainCam;
    [SerializeField] private Camera enerbeamCam;

    public PlayerStateManager playerState;
    public PlayerControll playerControll;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        CharacterManager.instance.SetCharacter(pos, MainCam, enerbeamCam);
    }

    private void Start()
    {
    }
}
