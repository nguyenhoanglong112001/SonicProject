using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] private int score;
    [SerializeField] public GameObject player;
    private Vector3 currentpos;
    private Vector3 lastPos;
    [SerializeField] private PlayerControll checkalive;
    [SerializeField] private MutiplyerScript mutiplyer;
    [SerializeField] public UnityEvent<int> OnScoreChange;
    private float distanceMove;
    [SerializeField] private float pointPerMove;
    public int Score 
    { 
        get => score; 
        set
        {
            score = value;
            OnScoreChange.Invoke(score);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPos = player.transform.position;
        Score = 0;
        OnScoreChange.Invoke(Score);
    }

    // Update is called once per frame
    void Update()
    {
        currentpos = player.transform.position;
        if(PlayerManager.instance.isAlive)
        {
            UpdateScoreByDistance();
        }
    }

    private void UpdateScoreByDistance()
    {
        float distanceFrame = Vector3.Distance(currentpos, lastPos);
        distanceMove += distanceFrame;

        if(distanceMove > 1)
        {
            if(CharacterManager.instance.bonusType == BonusType.DistanceScore)
            {
                Score += (int)(pointPerMove * mutiplyer.Mutiplyer * (CharacterManager.instance.bonus/100));
            }
            else
            {
                Score += (int)(pointPerMove * mutiplyer.Mutiplyer);
            }
            distanceMove -= 1;
        }

        lastPos = player.transform.position;

    }    

    public void UpdateScore(int point)
    {
        Score += (int)(point * mutiplyer.Mutiplyer);

        OnScoreChange.Invoke(Score);
    }

    public int GetScore()
    {
        return Score;
    }
}
    