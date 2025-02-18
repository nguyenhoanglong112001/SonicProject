using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] SwitchBall switchCheck;
    [SerializeField] PlayerStateManager checkcondition;

    private void OnCollisionEnter(Collision collision)
    {
        if (checkcondition.isjump)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                switchCheck.SwitchToCharacter();
                checkcondition.isjump = false;
            }
        }
    }
}
