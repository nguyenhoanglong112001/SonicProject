using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashPower : MonoBehaviour
{
    [SerializeField] private Animator playeranimator;
    [SerializeField] private InputManager speed;
    public bool isdashing;
    [SerializeField] private float duration;
    [SerializeField] private CollectManager check;

    [SerializeField] private float timeclick;
    [SerializeField] private float lastclicktime = -1;
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
        if(check.Energydash == 100)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(Time.time - lastclicktime < timeclick && !isdashing)
                {
                    isdashing = true;
                    playeranimator.SetBool("Dash", true);
                    speed.SpeedUp(1.5f);
                    StartCoroutine(DashTime());
                }
                lastclicktime = Time.time;
            }
        }
    }

    IEnumerator DashTime()
    {
        float energy = check.Energydash;
        float eslapedTime = 0;
        while (eslapedTime < duration)
        {
            eslapedTime += Time.deltaTime;
            check.Energydash = Mathf.Lerp(energy, 0, eslapedTime / duration);
            yield return null;
        }
        playeranimator.SetBool("Dash", false);
        speed.SpeedUp(1 / 1.5f);
        isdashing = false;
    }
}
