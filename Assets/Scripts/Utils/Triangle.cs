using UnityEngine;

public struct Triangle
{
    public Vector3 A, B, C;
    public Vector3 center;
    public Vector3 normal;

    public Triangle(Vector3 A, Vector3 B, Vector3 C)
    {
        this.A = A;
        this.B = B;
        this.C = C;
        center = (A + B + C) / 3;
        normal = Vector3.Cross(B - A, C - A).normalized;
    }
}