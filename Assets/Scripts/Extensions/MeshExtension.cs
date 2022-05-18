using UnityEngine;

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
}
