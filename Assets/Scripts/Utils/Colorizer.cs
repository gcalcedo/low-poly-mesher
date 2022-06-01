using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Colorizer
{
    public static Color FromHex(string hexColor)
    {
        if (!hexColor.StartsWith("#")) return Color.magenta;

        hexColor = hexColor.Substring(1);

        System.Globalization.NumberStyles format = System.Globalization.NumberStyles.HexNumber;
        float r = int.Parse(hexColor.Substring(0, 2), format) / 255f;
        float g = int.Parse(hexColor.Substring(2, 2), format) / 255f;
        float b = int.Parse(hexColor.Substring(4, 2), format) / 255f;
        float a = hexColor.Length == 8 ? int.Parse(hexColor.Substring(6, 2), format) / 255f : 1f;

        return new Color(r, g, b, a);
    }
}
