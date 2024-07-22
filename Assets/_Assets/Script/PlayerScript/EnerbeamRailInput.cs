using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnerbeamRailInput : MonoBehaviour
{
    [SerializeField] private float sensitive;
    [SerializeField] private Grind checkrail;
    [SerializeField] private CollectManager check;
    [SerializeField] private float minpitch;
    [SerializeField] private float maxpitch;
    [SerializeField] private float angleRotate;
    [SerializeField] private Animator playeranima;
    private float pitch;
    // Start is called before the first frame update
    void Start()
    {
        checkrail = GameObject.FindWithTag("Player").GetComponent<Grind>();
        check = GameObject.FindWithTag("Player").GetComponent<CollectManager>();
        playeranima = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateHorizontal();
    }

    private void RotateHorizontal()
    {
        if(checkrail.Israil && check.Isenerbeam)
        {
            float MouseX = Input.GetAxis("Mouse X");
            float deltaPitch = MouseX * angleRotate;
            pitch = Mathf.Clamp(pitch + deltaPitch, minpitch, maxpitch);
            transform.localEulerAngles = new Vector3(0, 0, pitch);
            Debug.Log(pitch);
            if(pitch > 0)
            {
                playeranima.SetBool("EnerRight", true);
                playeranima.SetBool("EnerLeft", false);
            }
            else if (pitch <0)
            {
                playeranima.SetBool("EnerRight", false);
                playeranima.SetBool("EnerLeft", true);
            }
            else if (pitch == 0)
            {
                playeranima.SetBool("EnerRight", false);
                playeranima.SetBool("EnerLeft", false);
            }
        }
    }
}
