using System.Collections.Generic;
using UnityEngine;
public static class PolyIO
{
    public static List<string> String(MeshPackage pointSet)
    {
        List<string> pointSetString = new List<string>();
        foreach(Vector3 v in pointSet.Vertices)
        {
            pointSetString.Add(
                v.x.ToString("0.00").Replace(',', '.') + " " +
                v.y.ToString("0.00").Replace(',', '.') + " " +
                v.z.ToString("0.00").Replace(',', '.'));
        }
        return pointSetString;
    }
}
