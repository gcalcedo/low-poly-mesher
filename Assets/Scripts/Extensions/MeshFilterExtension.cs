using UnityEngine;

public static class MeshFilterExtension
{
    /// <summary>
    /// Aplies <paramref name="meshData"/> to the <see cref="Mesh"/> of <paramref name="filter"/>.
    /// </summary>
    /// <param name="filter">The filter to contain the mesh.</param>
    /// <param name="meshData">The data of the mesh.</param>
    public static void LoadMeshData(this MeshFilter filter, MeshData meshData)
    {
        filter.mesh.Clear();
        filter.mesh.vertices = meshData.Vertices.ToArray();
        filter.mesh.triangles = meshData.Triangles.ToArray();
        filter.mesh.RecalculateNormals();
    }
}
