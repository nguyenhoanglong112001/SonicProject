using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private GameObject player;
    private Vector3 startpos;
    private Vector3 currentpos;
    [SerializeField] private Text scoreText;
    [SerializeField] private PlayerControll checkalive;
    [SerializeField] private MutiplyerScript mutiplyer;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        checkalive = GameObject.FindWithTag("Player").GetComponent<PlayerControll>();
        startpos = player.transform.position;
        score = 0;
        ShowScore();
    }

    // Update is called once per frame
    void Update()
    {
        currentpos = player.transform.position;
        if(checkalive.isalive)
        {
            score = (int)(Vector3.Distance(currentpos, startpos) * mutiplyer.Mutiplyer);
        }
        ShowScore();
    }

    public void UpdateScore(int point)
    {
        score += (int)(point * mutiplyer.Mutiplyer);
    }
    private void ShowScore()
    {
        scoreText.text = score.ToString();
    }
}
