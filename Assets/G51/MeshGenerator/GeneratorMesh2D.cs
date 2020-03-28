using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorMesh2D : MonoBehaviour
{
    private MeshFilter filter;
    public List<Vector2> dots;
    public bool snap;
    private void OnEnable()
    {
        Init();
    }

    private void LateUpdate()
    {
        Mesh m = new Mesh();
    }

    void Init()
    {
        filter = GetComponent<MeshFilter>();
        if (!filter)
            filter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer rend = GetComponent<MeshRenderer>();
        if (!rend)
            rend = gameObject.AddComponent<MeshRenderer>();
    }
}
