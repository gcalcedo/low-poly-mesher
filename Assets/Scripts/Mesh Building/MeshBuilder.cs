using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class MeshBuilder
{
    public static async Task<MeshData> BuildMeshAsync(MeshTemplate template, GameObject target)
    {
        PointSet pointSet = PointSet.Build(template);

        await PolyIO.ToXYZ(pointSet, target);

        Process mesher = new Process();
        mesher.StartInfo.FileName = "mesher";
        mesher.StartInfo.Arguments = target.GetInstanceID() + ".xyz";
        mesher.StartInfo.WorkingDirectory = Application.persistentDataPath;
        mesher.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        mesher.Start();
        await mesher.WaitForExitAsync();

        MeshData md = await PolyIO.FromOFF(target);

        //File.Delete(Application.persistentDataPath + "/" + target.GetInstanceID() + ".xyz");
        //File.Delete(Application.persistentDataPath + "/" + target.GetInstanceID() + ".off");

        if (md.HasInwardFaces())
        {
            md.FlipFaces();
        }

        return md;
    }

    public static async Task<MeshData> BuildMeshAsyncWithStream(MeshTemplate template)
    {
        PointSet pointSet = PointSet.Build(template);
        List<string> stringPointSet = PolyIO.String(pointSet);
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
     
        Process mesher = new Process();
        mesher.StartInfo = new ProcessStartInfo(Application.persistentDataPath + "/reconstruction_fct.exe");
        mesher.StartInfo.Arguments = "0 5.0 0.5 " + stringPointSet.Count;
        mesher.StartInfo.UseShellExecute = false;
        mesher.StartInfo.RedirectStandardOutput = true;
        mesher.StartInfo.RedirectStandardInput = true;
        mesher.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        mesher.StartInfo.CreateNoWindow = true;
        mesher.Start();

        StreamWriter streamWriter = mesher.StandardInput;
        mesher.OutputDataReceived += (sender, args) =>
        {
            string[] data = args.Data.Split(' ');
            switch (data[0])
            {
                case "P":
                    vertices.Add(new Vector3(
                        float.Parse(data[1].Replace(".", ",")),
                        float.Parse(data[2].Replace(".", ",")),
                        float.Parse(data[3].Replace(".", ","))));
                    break;
                case "F":
                    triangles.Add(int.Parse(data[1]));
                    triangles.Add(int.Parse(data[2]));
                    triangles.Add(int.Parse(data[3]));
                    break;
            }
            streamWriter.WriteLine();
        };
        mesher.BeginOutputReadLine();

        foreach(string s in stringPointSet)
        {
            streamWriter.WriteLine(s);
        }

        await mesher.WaitForExitAsync();

        List<Vector3> fullVertices = new List<Vector3>();
        foreach (int t in triangles)
        {
            fullVertices.Add(vertices[t]);
        }

        MeshData md = new MeshData(fullVertices.ToArray(), Enumerable.Range(0, triangles.Count).ToArray());

        if (md.HasInwardFaces())
        {
            md.FlipFaces();
        }

        return md;
    }
}



