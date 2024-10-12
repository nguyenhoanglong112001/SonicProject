using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DistanceUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text distanceText;
    [SerializeField] private GameObject player;
    private Vector3 startPos;
    private int distance;

    void Start()
    {
        startPos = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShowDistance();
    }

    private void ShowDistance()
    {
        distance = (int)Vector3.Distance(player.transform.position, startPos);
        distanceText.text = distance.ToString() + " m";
    }
}
