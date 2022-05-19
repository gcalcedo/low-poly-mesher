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
    private MeshData md;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        md = new MeshData();
        GenerateMesh();
    }
    private void Update()
    {
        if (md.IsAnimated)
        {
            Debug.Log("ANIMATED");
            mesh.SetVertices(md.Vertices);
            mesh.RecalculateNormals();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GenerateMesh();
        }
    }

    public async void GenerateMesh()
    {
        md = await MeshBuilder.BuildMeshData(template);
        Debug.Log(template.Mods.Count);
        mesh.LoadMeshData(md);
    }

    public IEnumerable<Type> GetInheritedClasses(Type MyType)
    {
        return Assembly.GetAssembly(MyType).GetTypes().Where(TheType =>
            TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(MyType));
    }
}

