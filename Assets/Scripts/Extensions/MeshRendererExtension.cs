using System.Collections.Generic;
using UnityEngine;

public static class MeshRendererExtension
{
    public static void LoadRendererData(this MeshRenderer meshRenderer, IList<MeshPackage> packages)
    {
        Material[] mats = new Material[packages.Count];
        for (int i = 0; i < packages.Count; i++)
        {
            mats[i] = packages[i].Material;
        }

        meshRenderer.materials = mats;
    }
}
