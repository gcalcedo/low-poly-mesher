public static class FloatExtension
{
    /// <summary>
    /// Maps this <see langword="float"/> from the range [<paramref name="fromMin"/>, <paramref name="fromMax"/>]
    /// to the range [<paramref name="toMin"/>, <paramref name="toMax"/>].
    /// </summary>
    /// <param name="from">The float to be mapped.</param>
    /// <param name="fromMin">Lower bound of the source range.</param>
    /// <param name="fromMax">Upper bound of the source range.</param>
    /// <param name="toMin">Lower bound of the target range.</param>
    /// <param name="toMax">Upper bound of the target range.</param>
    /// <returns></returns>
    public static float Map(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}

