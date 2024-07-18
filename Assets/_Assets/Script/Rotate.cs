using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float angleRotate;
    [SerializeField] private Transform cameraholder;
    [SerializeField] private float minpitch;
    [SerializeField] private float maxpitch;
    private float pitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateHorizontal();
    }

    private void RotateHorizontal()
    {
        float mouseX = Input.GetAxis("Mouse X");

        float yaw = mouseX * angleRotate;
        transform.Rotate(0, yaw, 0);
    }
}
