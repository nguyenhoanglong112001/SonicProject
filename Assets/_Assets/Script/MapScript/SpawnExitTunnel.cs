using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExitTunnel : MonoBehaviour
{
    [SerializeField] private List<GameObject> exitTunnel;
    public Transform spawnPos;
    public Transform Rotation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameObject exit = exitTunnel[Random.Range(0, exitTunnel.Count)];
            GameObject exitRoad = Instantiate(exit, spawnPos.position, exit.transform.rotation);
            StartCoroutine(RotateRoad(exitRoad));
        }
    }

    IEnumerator RotateRoad(GameObject road)
    {
        yield return new WaitForSeconds(1.0f);
        road.transform.rotation = spawnPos.rotation;
    }
}
