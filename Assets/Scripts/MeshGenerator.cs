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
        if (Input.GetKeyDown(KeyCode.T))
        {
            GenerateMesh();
        }

        mesh.MarkDynamic();
        mesh.SetVertices(md.Vertices);
        mesh.RecalculateNormals();
    }

    public async void GenerateMesh()
    {
        md = await MeshBuilder.BuildMeshData(template);
        mesh.LoadMeshData(md);


        for (int i = 0; i < md.Vertices.Count; i++)
        {
            int vertexId = i;
            DOTween.To(
                () => md.Vertices[vertexId],
                (v) => md.Vertices[vertexId] = v,
                new Vector3(0, 2, 0),
                2
                )
                .SetRelative()
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    public IEnumerable<Type> GetInheritedClasses(Type MyType)
    {
        return Assembly.GetAssembly(MyType).GetTypes().Where(TheType =>
            TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(MyType));
    }
}

