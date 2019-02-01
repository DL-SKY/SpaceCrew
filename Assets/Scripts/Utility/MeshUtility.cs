using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{
    public static class MeshUtility
    {       
        public static Mesh CreateMesh(Mesh _mesh)
        {
            Mesh m = new Mesh();
            m.name = "ScriptedMesh";
            List<int> triangles = new List<int>();

            var vertices = _mesh.vertices.Select(x => new MeshVertex(x)).ToList();

            var result = MIConvexHull.ConvexHull.Create(vertices).Result;
            m.vertices = result.Points.Select(x => x.ToVec()).ToArray();
            var xxx = result.Points.ToList();

            foreach (var face in result.Faces)
            {
                triangles.Add(xxx.IndexOf(face.Vertices[0]));
                triangles.Add(xxx.IndexOf(face.Vertices[1]));
                triangles.Add(xxx.IndexOf(face.Vertices[2]));
            }

            m.triangles = triangles.ToArray();

            m.RecalculateNormals();
            m.RecalculateTangents();
            m.RecalculateBounds();

            return m;
        }
    }
}
