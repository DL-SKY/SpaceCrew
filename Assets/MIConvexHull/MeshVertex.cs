using MIConvexHull;
using UnityEngine;

public class MeshVertex : IVertex
{
    public double[] Position { get; set; }

    public MeshVertex(double _x, double _y, double _z)
    {
        Position = new double[3] { _x, _y, _z };
    }

    public MeshVertex(Vector3 _ver)
    {
        Position = new double[3] { _ver.x, _ver.y, _ver.z };
    }

    public Vector3 ToVec()
    {
        return new Vector3((float)Position[0], (float)Position[1], (float)Position[2]);
    }
}
