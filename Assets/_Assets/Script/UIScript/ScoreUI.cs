using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager.OnScoreChange.AddListener(UpdateScoreUI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScoreUI(int score)
    {
        scoreText.text = score.ToString();
    }    
}
