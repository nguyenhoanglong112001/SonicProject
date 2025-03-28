using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;
    public UnityEvent OnComboChange;
    [SerializeField] private ScoreManager score;
    [SerializeField] private float comboTime;
    [SerializeField] private int comboAdd;
    [SerializeField] private GameObject comboUI;
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

    public float RemainTime { get => remainTime; set => remainTime = value; }
    public float ComboTime { get => comboTime; set => comboTime = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RemainTime = ComboTime;
        ComboCount = 0;
        OnComboChange.AddListener(CountDown);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCombo()
    {
        comboCount += comboAdd;
        if(comboCount %10 ==0 && comboCount >0)
        {
            if(CharacterManager.instance.bonusType == BonusType.ComboBonus)
            {
                bonusScore = bonusScore + (10 * CharacterManager.instance.bonus / 100);
            }
            else
            {
                bonusScore += 10;
            }
        }
        score.UpdateScore(bonusScore);
        OnComboChange.Invoke();
    }

    IEnumerator ComboCountDown()
    {
        RemainTime = ComboTime;
        comboUI.SetActive(true);
        while (RemainTime > 0)
        {
            RemainTime -= Time.deltaTime;
            yield return null;
        }
        comboUI.SetActive(false);
        comboCount = 0;
    }

    public void CountDown()
    {
        StopCoroutine(ComboCountDown());
        StartCoroutine(ComboCountDown());
    }
}
