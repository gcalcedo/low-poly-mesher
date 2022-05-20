using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class MeshExtension
{
    /// <summary>
    /// Aplies <paramref name="meshData"/> to this <see cref="Mesh"/>.
    /// </summary>
    /// <param name="mesh">The mesh on which to load the data.</param>
    /// <param name="meshData">The data of the mesh.</param>
    public static void LoadMeshData(this Mesh mesh, MeshData meshData)
    {
        mesh.Clear();
        mesh.vertices = meshData.Vertices.ToArray();
        mesh.triangles = meshData.Triangles.ToArray();
        mesh.RecalculateNormals();
    }

    public static void LoadMeshData(this Mesh mesh, IList<MeshData> meshData)
    {
        mesh.Clear();
        mesh.subMeshCount = meshData.Count;
        List<Vector3> vertices = new List<Vector3>();
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            mesh.GetVertices(vertices);
            int preVertexCount = vertices.Count;
            vertices.AddRange(meshData[i].Vertices);
            mesh.SetVertices(vertices);
            for(int j = 0; j < meshData[i].Triangles.Count; j++)
            {
                meshData[i].Triangles[j] += preVertexCount;
            }
            mesh.SetTriangles(meshData[i].Triangles, i);
        }
        mesh.RecalculateNormals();
    }

    public static void UpdateMeshVertices(this Mesh mesh, IList<MeshData> meshData)
    {
        List<Vector3> vertices = new List<Vector3>();
        foreach (MeshData md in meshData)
        {
            vertices.AddRange(md.Vertices);
        }
        mesh.SetVertices(vertices);
    }
}
