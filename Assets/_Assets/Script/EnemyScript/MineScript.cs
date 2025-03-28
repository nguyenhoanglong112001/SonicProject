using UnityEngine;

public class MineScript : MonoBehaviour
{
    public AudioSource hitSound;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            hitSound.Play();
        }
    }
}
