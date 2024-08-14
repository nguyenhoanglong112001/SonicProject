using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBall : MonoBehaviour
{
    [SerializeField] private Animator playeranimator;
    [SerializeField] private SkinnedMeshRenderer mesh;
    [SerializeField] private Material ballmaterial;
    [SerializeField] private Material characterMaterial;
    [SerializeField] private Mesh ballmesh;
    [SerializeField] private Mesh charactermesh;
    [SerializeField] private Avatar characterAvatar;
    [SerializeField] private Avatar ballAvatar;
    [SerializeField] private RuntimeAnimatorController ballanimator;
    [SerializeField] private RuntimeAnimatorController characteranimator;
    [SerializeField] private CapsuleCollider charactercollider;
    [SerializeField] private SphereCollider ballcollider;
    [SerializeField] private InputManager checkcondition;
    [SerializeField] private Grind checkrail;
    [SerializeField] private CollectManager checkcollect;

    public bool isball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Switch(Mesh meshchange, Material material, RuntimeAnimatorController animator, Avatar avatar, bool active1, bool active2)
    {
        mesh.sharedMesh = meshchange;
        mesh.material = material;
        playeranimator.runtimeAnimatorController = animator;
        playeranimator.avatar = avatar;
        ballcollider.enabled = active1;
        charactercollider.enabled = active2;
    }

    public void ChangeBall()
    {
        Switch(ballmesh, ballmaterial, ballanimator, ballAvatar, true, false);
        isball = true;
    }

    public void SwitchToCharacter()
    {
        Switch(charactermesh, characterMaterial, characteranimator, characterAvatar, false, true);
        isball = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(checkcondition.Isjumping)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                SwitchToCharacter();
                checkcondition.Isjumping = false;
            }
        }
    }
}
