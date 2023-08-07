using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshOptimizer : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        mesh.Optimize();
    }
}
