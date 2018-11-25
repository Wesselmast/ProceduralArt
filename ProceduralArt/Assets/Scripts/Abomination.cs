using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abomination : MonoBehaviour {

    [SerializeField]
    private float bpm;
    private Mesh mesh;

    private Vector3[] beginVertices;
    float scale = 1.3f;

    void Start() {
        mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        Application.targetFrameRate = 60;
        beginVertices = mesh.vertices;
        StartCoroutine(Glitch());
    }

    IEnumerator Glitch() {
        mesh.vertices = GlitchVertices();
        mesh.RecalculateBounds();
        yield return new WaitForSeconds(60.0f / bpm);
        mesh.vertices = beginVertices;
        mesh.RecalculateBounds();
        yield return new WaitForSeconds(60.0f / bpm);
        StartCoroutine(Glitch());
    }

    Vector3[] GlitchVertices() {
        List<Vector3> vertices = new List<Vector3>(mesh.vertices);
        for (int i = 0; i < Random.Range(10, 20); i++) {
            var vertex = beginVertices[i];      
            vertex.x = vertex.x * scale;
            vertex.y = vertex.y * scale;
            vertex.z = vertex.z * scale;
            vertices[i] = vertex;
        }
        return vertices.ToArray();
    }
}