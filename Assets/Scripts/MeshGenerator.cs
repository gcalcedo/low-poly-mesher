using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    [HideInInspector] public string templateAssemblyName = "";
    [SerializeReference] public MeshTemplate template;

    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private IList<MeshData> meshData;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer = GetComponent<MeshRenderer>();
        meshData = new List<MeshData>();
        GenerateMesh();
    }
    private void Update()
    {
        mesh.MarkDynamic(); 
        mesh.UpdateMeshVertices((IList<MeshData>)meshData);
        mesh.RecalculateNormals();

        if (Input.GetKeyDown(KeyCode.T))
        {
            mesh.Clear();
            GenerateMesh();
        }
    }

    public async void GenerateMesh()
    {
        meshData = (IList<MeshData>)await MeshBuilder.BuildMeshData(template);
        mesh.LoadMeshData((IList<MeshData>)meshData);
        meshRenderer.materials = Enumerable.Repeat(meshRenderer.materials[0], mesh.subMeshCount).ToArray();

        foreach (MeshData md in meshData)
        {
            md.LaunchAnimation();
        }
    }

    public IEnumerable<Type> GetInheritedClasses(Type MyType)
    {
        return Assembly.GetAssembly(MyType).GetTypes().Where(TheType =>
            TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(MyType));
    }
}

