using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class MeshBuilder
{
    public static async Task<ICollection<MeshPackage>> BuildMeshTemplate(MeshTemplate template)
    {
        List<MeshPackage> meshes = new List<MeshPackage>();
        foreach(MeshPackage package in MeshPackage.Build(template))
        {
            meshes.Add(await BuildMeshAsync(package));
        }
        return meshes;
    }

    private static async Task<MeshPackage> BuildMeshAsync(MeshPackage package)
    {
        List<string> stringPointSet = PolyIO.String(package);
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

        return new MeshPackage(fullVertices.ToArray(), Enumerable.Range(0, triangles.Count).ToArray(), package.Animation);
    }
}



