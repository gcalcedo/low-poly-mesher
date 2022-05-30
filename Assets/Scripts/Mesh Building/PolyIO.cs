using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
public static class PolyIO
{
    public static List<string> String(VertexData pointSet)
    {
        List<string> pointSetString = new List<string>();
        foreach(Vector3 v in pointSet.Set)
        {
            pointSetString.Add(
                v.x.ToString("0.00").Replace(',', '.') + " " +
                v.y.ToString("0.00").Replace(',', '.') + " " +
                v.z.ToString("0.00").Replace(',', '.'));
        }
        return pointSetString;
    }

    public static async Task ToXYZ(VertexData pointSet, GameObject target)
    {
        using StreamWriter file = new StreamWriter(Application.persistentDataPath + "/" + target.GetInstanceID() + ".xyz");
        foreach (Vector3 v in pointSet.Set)
        {
            await file.WriteLineAsync(
                v.x.ToString("0.00").Replace(',', '.') + " " +
                v.y.ToString("0.00").Replace(',', '.') + " " +
                v.z.ToString("0.00").Replace(',', '.'));
        }
    }

    public static async Task<MeshPackage> FromOFF(GameObject target)
    {
        using StreamReader file = new StreamReader(Application.persistentDataPath + "/" + target.GetInstanceID() + ".off");
        string fullFile = await file.ReadToEndAsync();
        string[] lines = fullFile.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        //string[] lines = File.ReadAllLines(Application.persistentDataPath + "/" + target.GetInstanceID() + ".off");
        int numberOfVertices = int.Parse(lines[1].Split()[0]);
        int numberOfTriangles = int.Parse(lines[1].Split()[1]);

        Vector3[] vertices = new Vector3[numberOfVertices];
        ICollection<int> triangles = new List<int>();

        for (int i = 2; i < 2 + numberOfVertices; i++)
        {
            string[] point = lines[i].Split();
            vertices[i - 2] = new Vector3(
                float.Parse(point[0].Replace(".", ",")),
                float.Parse(point[1].Replace(".", ",")),
                float.Parse(point[2].Replace(".", ",")));
        }

        for (int i = 2 + numberOfVertices; i < 2 + numberOfVertices + numberOfTriangles; i++)
        {
            string[] triangle = lines[i].Split();
            for (int j = 1; j < 4; j++)
            {
                triangles.Add(int.Parse(triangle[j]));
            }
        }

        List<Vector3> fullVertices = new List<Vector3>();
        foreach (int t in triangles)
        {
            fullVertices.Add(vertices[t]);
        }

        return new MeshPackage(fullVertices.ToArray(), Enumerable.Range(0, numberOfTriangles * 3).ToArray());
    }
}
