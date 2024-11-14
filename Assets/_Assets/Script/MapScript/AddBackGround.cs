using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBackGround : MonoBehaviour
{
    [SerializeField] private GameObject BackGround;
    [SerializeField] private CheckLane checkEnd;
    private void Start()
    {
        if(!checkEnd.isEndZone)
        {
            Instantiate(BackGround, transform.position, BackGround.transform.rotation);
        }
    }
}
