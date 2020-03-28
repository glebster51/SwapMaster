using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdDifFinder : MonoBehaviour
{
    public MeshFilter a, b;
    public MeshFilter f;
    [Range(0f,1f)]
    public float progress;
    public Vector3 centreOffset;
    public Vector3 scale;
    public float size = 1f;
    public bool active;

    private void OnDrawGizmos()
    {
        if (!active)
        {
                   return;
        }
        if (a && b)
        {
            for (int i = 0; i < a.mesh.vertexCount; i++)
            {
                //Vector3 ga = a.mesh.vertices[i] + a.transform.position;
                // gb = b.mesh.vertices[i] + b.transform.position;
                //Gizmos.color = new Color(1,1,1, 0.3f);
               // Gizmos.DrawLine(ga, gb);
            }
        }

        if (f)
        {
            Mesh m = new Mesh();
            //m.SetVertices(a.mesh.vertices);
            List<Vector3> v3 = new List<Vector3>();
            Mesh original = a.sharedMesh;
            Mesh target = b.sharedMesh;
            for (int i = 0; i < original.vertexCount; i++)
            {
                Vector3 or = original.vertices[i];
                Vector3 ner;
                for (int j = 0; j < target.vertexCount; j++)
                {
                    
                }
            }
            
            m.SetVertices(v3);
            m.SetNormals(original.normals);
            m.SetTriangles(original.triangles,0);
            m.SetUVs(0,original.uv);
            f.GetComponent<MeshRenderer>().sharedMaterial = a.GetComponent<MeshRenderer>().sharedMaterial;
            f.mesh = m;
        }
    }
}
