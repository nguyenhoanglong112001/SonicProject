using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboManager : MonoBehaviour
{
    public UnityEvent OnComboChange;
    [SerializeField] private ScoreManager score;
    [SerializeField] private float comboTime;
    [SerializeField] private int comboAdd;
    private float remainTime;
    private int comboCount;
    [SerializeField] private int bonusScore;

    public int ComboCount
    { 
        get => comboCount; 
        set
        {
            comboCount = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        remainTime = comboTime;
        ComboCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCombo()
    {
        comboCount += comboAdd;
        remainTime = comboTime;
        score.UpdateScore(bonusScore);
        OnComboChange.Invoke();
    }
}
