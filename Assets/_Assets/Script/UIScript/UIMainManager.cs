using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainManager : MonoBehaviour
{
    [SerializeField] private Button startBt;

    private void Start()
    {
        startBt.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        GameManager.instance.ChangeGameState(GameState.InGame);
    }
}
