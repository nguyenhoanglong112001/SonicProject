using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopObject : MonoBehaviour
{
    [SerializeField] private PlayerStateManager player;
    [SerializeField] private float speedUp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(SoundManager.instance.pickUpSound, SoundManager.instance.hoopSound);
            player.follower.followSpeed += speedUp;
        }
    }
}
