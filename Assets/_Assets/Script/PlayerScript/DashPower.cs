using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPower : MonoBehaviour
{
    [SerializeField] private Animator playeranimator;
    [SerializeField] private InputManager speed;
    public bool isdashing;
    [SerializeField] private float time; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
    }

    private void Dash()
    {
        if(Input.GetKeyDown(KeyCode.F) && !isdashing)
        {
            playeranimator.SetBool("Dash", true);
            speed.SpeedUp(1.5f);
            StartCoroutine(DashTime());
        }
    }

    IEnumerator DashTime()
    {
        isdashing = true;
        yield return new WaitForSeconds(time);
        playeranimator.SetBool("Dash", false);
        speed.SpeedUp(1/1.5f);
        isdashing = false;
    }
}
