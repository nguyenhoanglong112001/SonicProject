using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshsize;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 size = meshsize.bounds.size;
        Debug.Log("size y: " + size.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
