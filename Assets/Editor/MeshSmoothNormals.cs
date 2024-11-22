using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    public static partial class MeshTools
    {
        private const float MaxVertexDistance = 0.001f;

        private class Vertex
        {
            public Vector3 position;
            public Vector3 normal;
        }

        [MenuItem("Assets/Game/Mesh/Smooth Normals")]
        public static void MenuSmooth()
        {
            var selections = Selection.objects;
            if (selections == null || selections.Length == 0)
            {
                Debug.LogError("No object selected object to alter");
                return;
            }

            foreach (var selection in selections)
            {
                if (selection is not Mesh mesh)
                {
                    Debug.LogError($"Selected object {selection.name} is not Mesh");
                    return;
                }

                ToggleReadable(mesh);

                AlterNormals(mesh);

                ToggleReadable(mesh);

                const string POSTFIX = "_Smoothed";

                var assetPath = AssetDatabase.GetAssetPath(mesh);
                if (!mesh.name.Contains(POSTFIX))
                {
                    AssetDatabase.RenameAsset(assetPath, mesh.name + POSTFIX);
                }
                AssetDatabase.SaveAssetIfDirty(mesh);
            }
        }

        [MenuItem("Assets/Game/Mesh/Outline Normals")]
        public static void MenuOutline()
        {
            var selections = Selection.objects;
            if (selections == null || selections.Length == 0)
            {
                Debug.LogError("No object selected object to alter");
                return;
            }

            foreach (var selection in selections)
            {
                if (selection is not Mesh mesh)
                {
                    Debug.LogError($"Selected object {selection.name} is not Mesh");
                    return;
                }

                ToggleReadable(mesh);

                AlterNormals(mesh, 3);

                ToggleReadable(mesh);

                const string POSTFIX = "_Outlines";

                var assetPath = AssetDatabase.GetAssetPath(mesh);
                if (!mesh.name.Contains(POSTFIX))
                {
                    AssetDatabase.RenameAsset(assetPath, mesh.name + POSTFIX);
                }
                AssetDatabase.SaveAssetIfDirty(mesh);
            }
        }

        public static void AlterNormals(Mesh mesh, uint channel = 0)
        {
            var vertices = mesh.vertices;

            var cospatialVerticesData = new List<Vertex>();
            var cospacialVertexIndices = new int[vertices.Length];

            FindCospatialVertices(vertices, cospacialVertexIndices, cospatialVerticesData);

            for (int i = 0; i < mesh.subMeshCount; ++i)
            {
                var triangles = mesh.GetTriangles(i);

                int numTriangles = triangles.Length / 3;
                for (int t = 0; t < numTriangles; t++)
                {
                    int t0 = t * 3;
                    int v0 = triangles[t0];
                    int v1 = triangles[t0 + 1];
                    int v2 = triangles[t0 + 2];

                    var normal = ComputeNormal(vertices[v0], vertices[v1], vertices[v2]);
                    var weights = ComputeWeights(vertices[v0], vertices[v1], vertices[v2]);

                    AddNormal(v0, normal * weights.x, cospacialVertexIndices, cospatialVerticesData);
                    AddNormal(v1, normal * weights.y, cospacialVertexIndices, cospatialVerticesData);
                    AddNormal(v2, normal * weights.z, cospacialVertexIndices, cospatialVerticesData);
                }
            }

            if (channel > 0)
            {
                var normals = new Vector4[vertices.Length];
                for (int v = 0; v < normals.Length; v++)
                {
                    int index = cospacialVertexIndices[v];
                    var data = cospatialVerticesData[index];

                    normals[v] = data.normal.normalized;
                }

                mesh.SetUVs((int)channel, normals);
            }
            else
            {
                var normals = new Vector3[vertices.Length];
                for (int v = 0; v < normals.Length; v++)
                {
                    int index = cospacialVertexIndices[v];
                    var data = cospatialVerticesData[index];

                    normals[v] = data.normal.normalized;
                }

                mesh.SetNormals(normals);
            }
        }

        private static void FindCospatialVertices(Vector3[] vertices, int[] indices, List<Vertex> data)
        {
            for (int v = 0; v < vertices.Length; v++)
            {
                if (SarchForClosestEntry(vertices[v], data, out int index))
                {
                    indices[v] = index;
                }
                else
                {
                    var entry = new Vertex()
                    {
                        position = vertices[v],
                        normal = Vector3.zero,
                    };

                    indices[v] = data.Count;
                    data.Add(entry);
                }
            }
        }

        private static bool SarchForClosestEntry(Vector3 position, List<Vertex> data, out int index)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (Vector3.Distance(data[i].position, position) <= MaxVertexDistance)
                {
                    index = i;
                    return true;
                }
            }
            index = -1;
            return false;
        }

        private static Vector3 ComputeNormal(Vector3 a, Vector3 b, Vector3 c)
        {
            var cross = Vector3.Cross(b - a, c - a);
            cross.Normalize();
            return cross;
        }

        private static Vector3 ComputeWeights(Vector3 a, Vector3 b, Vector3 c)
        {
            var w0 = Vector3.Angle(b - a, c - a);
            var w1 = Vector3.Angle(c - b, a - b);
            var w2 = Vector3.Angle(a - c, b - c);
            return new Vector3(w0, w1, w2);
        }

        private static void AddNormal(int vertexIndex, Vector3 vertexNormal, int[] indices, List<Vertex> data)
        {
            int index = indices[vertexIndex];
            data[index].normal += vertexNormal;
        }
    }
}