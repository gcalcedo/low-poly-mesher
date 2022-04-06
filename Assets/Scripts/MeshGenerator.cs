using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    [HideInInspector] public string templateAssemblyName = "";

    [SerializeReference]
    public MeshTemplate template;

    private void Start()
    {
        GenerateMesh();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GenerateMesh();
        }
    }

    public async void GenerateMesh()
    {
        //MeshData md = await MeshBuilder.BuildMeshAsync(template, gameObject);
        MeshData md = await MeshBuilder.BuildMeshAsyncWithStream(template);
        GetComponent<MeshFilter>().LoadMeshData(md);
    }

    public IEnumerable<Type> GetInheritedClasses(Type MyType)
    {
        return Assembly.GetAssembly(MyType).GetTypes().Where(TheType =>
            TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(MyType));
    }
}

