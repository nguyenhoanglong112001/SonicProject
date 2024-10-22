using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    [SerializeField] private Text comboText;
    [SerializeField] private Text comboTypeText;
    [SerializeField] private Image comboCountDown;
    [SerializeField] private ComboManager combo;
    // Start is called before the first frame update
    void Awake()
    {
        combo.OnComboChange.AddListener(ShowCombo);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownCombo();
    }

    public void ShowCombo()
    {
        Debug.Log("show");
        gameObject.SetActive(true);
        comboText.text = combo.ComboCount.ToString();
    }

    public void ShowCombotype(string comboType)
    {
        comboTypeText.text = comboType +  "!";
    }

    public void CountDownCombo()
    {
        comboCountDown.fillAmount = combo.RemainTime / combo.ComboTime;
    }    
}