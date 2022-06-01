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
    private List<MeshPackage> meshData;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer = GetComponent<MeshRenderer>();
        meshData = new List<MeshPackage>();
        GenerateMesh();
    }
    private void Update()
    {
        mesh.MarkDynamic(); 
        mesh.UpdateMeshVertices(meshData);
        mesh.RecalculateNormals();

        if (Input.GetKeyDown(KeyCode.T))
        {
            mesh.Clear();
            GenerateMesh();
        }
    }

    public async void GenerateMesh()
    {
        meshData.ForEach(md => md.ClearAnimations());
        meshData = (List<MeshPackage>)await MeshBuilder.BuildMeshTemplate(template);
        mesh.LoadMeshData(meshData);
        meshRenderer.LoadRendererData(meshData);     

        foreach (MeshPackage md in meshData)
        {
            md.LaunchAnimation();
        }
    }
}

