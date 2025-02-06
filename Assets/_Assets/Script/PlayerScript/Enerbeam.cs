using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enerbeam : MonoBehaviour
{
    [SerializeField] private Rigidbody rigi;
    public PlayerStateManager player;
    public Tween currentTween;
    public Transform[] path;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerStateManager>();
        rigi.isKinematic = true;
        rigi.useGravity = false;
        MoveOnRail();
        this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void MoveOnRail()
    {
        if (path != null)
        {
            Vector3[] pointPath = System.Array.ConvertAll(path, t => t.position);
            if (pointPath[0] == transform.position)
            {
                List<Vector3> tempPath = new List<Vector3>(pointPath);
                tempPath.RemoveAt(0);
                pointPath = tempPath.ToArray();
            }
            currentTween = transform.DOPath(pointPath, player.enerbeamDuration, PathType.CatmullRom, PathMode.Full3D)
                .SetLookAt(0.01f)
                .OnComplete(() =>
                {
                    player.newState = player.state.Fall();
                    player.SwitchState(player.newState);
                });
        }
    }
}
