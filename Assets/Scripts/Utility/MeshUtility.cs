using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

            //m.uv uv = new Vector2[m.uv.Length];
            //_uv.CopyTo(m.uv, 0);
            Debug.Log("_UV: " + _mesh.uv.Length);

            m.RecalculateTangents();
            m.RecalculateBounds();
            //List<Vector2> uvs = new List<Vector2>();
            //_mesh.GetUVs(0, uvs);
            //m.SetUVs(0, uvs);
            //_mesh.uv.CopyTo(m.uv, 0);

            //m.
            m.uv = new Vector2[m.vertices.Length];
            for (int i = 0; i < m.uv.Length; i++)
                m.uv[i] = m.vertices[i];

            m.RecalculateTangents();
            m.RecalculateBounds();

            Debug.Log("UV: " + m.uv.Length);

            return m;
        }
    }
}
