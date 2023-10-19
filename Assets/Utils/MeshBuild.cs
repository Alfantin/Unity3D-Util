using System.Collections.Generic;
using UnityEngine;

public class MeshBuild{

    private List<Vector3> Vertices = new List<Vector3>();
    private List<Vector3> Normals = new List<Vector3>();
    private List<Color> Colors = new List<Color>();
    private List<Vector2> Uvs = new List<Vector2>();
    private List<int> Indices = new List<int>();

    public void Clear() {
        Vertices.Clear();
        Normals.Clear();
        Colors.Clear();
        Uvs.Clear();
        Indices.Clear();
    }

    public Mesh CreateMesh() {
        var ret = new Mesh();
        FillMesh(ret);
        return ret;
    }

    public void FillMesh(Mesh mesh) {
        mesh.vertices = Vertices.ToArray();
        mesh.normals = Normals.ToArray();
        mesh.colors = Colors.ToArray();
        mesh.uv = Uvs.ToArray();
        mesh.triangles = Indices.ToArray();
    }

    public void AddVertex(int x, int y, int z) {
        Vertices.Add(new Vector3(x, y, z));
    }

    public void AddVertex(Vector3 vertex) {
        Vertices.Add(vertex);
    }

    public void AddNormal(int x, int y, int z) {
        Normals.Add(new Vector3(x, y, z));
    }

    public void AddNormal(Vector3 normal) {
        Normals.Add(normal);
    }

    public void AddColor(float r, float g, float b, float a) {
        Colors.Add(new Color(r, g, b, a));
    }

    public void AddColor(Color color) {
        Colors.Add(color);
    }

    public void AddUv(float x, float y) {
        Uvs.Add(new Vector2(x, y));
    }

    public void AddUv(Vector2 uv) {
        Uvs.Add(uv);
    }

    public void AddIndex(int index) {
        Indices.Add(index);
    }

    public void CreateTriangle() {
        var vertexCount = Vertices.Count;
        Indices.Add(vertexCount - 3);
        Indices.Add(vertexCount - 2);
        Indices.Add(vertexCount - 1);
    }

    public void CreateQuad() {
        var vertexCount = Vertices.Count;
        Indices.Add(vertexCount - 4);
        Indices.Add(vertexCount - 3);
        Indices.Add(vertexCount - 2);
        Indices.Add(vertexCount - 4);
        Indices.Add(vertexCount - 2);
        Indices.Add(vertexCount - 1);
    }

}