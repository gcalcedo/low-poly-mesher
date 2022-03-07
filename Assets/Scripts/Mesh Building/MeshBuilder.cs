using System.Diagnostics;
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
}



