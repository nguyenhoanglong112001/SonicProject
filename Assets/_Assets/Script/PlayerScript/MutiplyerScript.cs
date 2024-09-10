using UnityEngine;
using UnityEngine.UI;

public class MutiplyerScript : MonoBehaviour
{
    [SerializeField] private float mutiplyer;
    [SerializeField] private CollectManager checkCollect;
    [SerializeField] private Text mutiplyText;

    public float Mutiplyer { get => mutiplyer; set => mutiplyer = value; }

    private void Start()
    {
        checkCollect = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
    }

    private void Update()
    {
        ShowMutiply();
    }

    private void ShowMutiply()
    {
        mutiplyText.text = "X" + Mutiplyer.ToString();
    }
}
