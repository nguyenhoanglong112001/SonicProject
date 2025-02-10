using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadType : MonoBehaviour
{
    public MeshRenderer roadMaterial;
    public MeshFilter roadMesh;
    public MeshFilter signMesh;
    public MeshRenderer signMat;
    // Material
    public Material zone1Mat;
    public Material zone2Mat;
    public Material zone4Mat;

    private Dictionary<Zone, Material> matDict = new Dictionary<Zone, Material>();

    public Mesh zone1Mesh;
    public Mesh zone2Mesh;
    public Mesh zone4Mesh;
    private Dictionary<Zone, Mesh> meshDict = new Dictionary<Zone, Mesh>();

    public Mesh zone1MeshSign;
    public Mesh zone2MeshSign;
    public Mesh zone4MeshSign;

    private Dictionary<Zone, Mesh> meshSignDict;

    private void Start()
    {
        matDict = new Dictionary<Zone, Material>()
        {
            {Zone.Zone1,zone1Mat },
            {Zone.Zone2,zone2Mat },
            {Zone.Zone4,zone4Mat }
        };

        meshDict = new Dictionary<Zone, Mesh>()
        {
            {Zone.Zone1,zone1Mesh},
            {Zone.Zone2,zone2Mesh},
            {Zone.Zone4,zone4Mesh}
        };
        if (signMesh != null && signMat != null)
        {
            meshSignDict = new Dictionary<Zone, Mesh>()
        {
            {Zone.Zone1,zone1MeshSign},
            {Zone.Zone2,zone2MeshSign},
            {Zone.Zone4,zone4MeshSign}
        };
        }
        ChangeRoad();
    }

    public void ChangeRoad()
    {
        roadMaterial.sharedMaterial = matDict[ZoneManager.instance.currentZone];
        roadMesh.mesh = meshDict[ZoneManager.instance.currentZone];
        if(signMesh != null && signMat != null)
        {
            signMesh.mesh = meshSignDict[ZoneManager.instance.currentZone];
            signMat.sharedMaterial = matDict[ZoneManager.instance.currentZone];
        }
    }

    private void ChangeMeshBound(Mesh mesh1,Mesh targetMesh)
    {
        Vector3 referenceCenter = mesh1.bounds.center;
        Vector3 offset = referenceCenter - targetMesh.bounds.center;

        Vector3[] vertices = targetMesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] += offset;
        }

        targetMesh.vertices = vertices;
        targetMesh.RecalculateBounds();
    }
}
