using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(GeneratorMesh2D))]
public class MeshGeneratorEditor : Editor
{
    private void OnSceneGUI()
    {
        GeneratorMesh2D gen = target as GeneratorMesh2D;
        
        EditorGUI.BeginChangeCheck();
        FindMidResult near = FindMid(gen);
        Handles.DrawWireDisc(near.point,Vector3.back, 0.05f);
        if (near.createMid)   // проверка на добавление новой
        {
            if (Event.current.OnKeyDown(KeyCode.M)){
                Undo.RecordObject(gen, "Create Mid Point");
                gen.dots.Add(gen.transform.InverseTransformPoint(near.point));
                for (int i = gen.dots.Count - 1; i > near.bi; i--)
                {
                    Vector2 a = gen.dots[i];
                    Vector2 b = gen.dots[i - 1];
                    gen.dots[i] = b;
                    gen.dots[i - 1] = a;
                }
                EditorUtility.SetDirty(gen);
            }
        }
        else
        {
            Vector3 dot = gen.transform.TransformPoint(gen.dots[near.bi]);
            Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? gen.transform.rotation : Quaternion.identity;
            dot = Handles.DoPositionHandle(dot, handleRotation);
            dot = gen.transform.InverseTransformPoint(dot);
            if (gen.snap)
            {
                float x = Mathf.Ceil(dot.x * 10f) / 10f;
                float y = Mathf.Ceil(dot.y * 10f) / 10f;
                dot = new Vector3(x, y);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(gen, "Move Point");
                EditorUtility.SetDirty(gen);
                gen.dots[near.bi] = dot;
            }
        }
        
        for (int i = 1; i <= gen.dots.Count; i++)     // Рисовать контур
        {
            Vector3 a = gen.transform.TransformPoint(gen.dots[i - 1]);
            Vector3 b = gen.transform.TransformPoint(gen.dots[i % gen.dots.Count]);
            Handles.DrawLine(a, b);
        }
    }

    FindMidResult FindMid(GeneratorMesh2D gen)
    {
        Vector3 mousePos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
        Vector3 mousePosLocal = gen.transform.InverseTransformPoint(mousePos);

        int i = 1;
        float dist = 99999999999f;
        for (int j = 1; j <= gen.dots.Count; j++)
        {
            int jj = j;
            float d = SegPointDist(mousePosLocal, gen.dots[jj - 1], gen.dots[jj % (gen.dots.Count)]);
            if (d <= dist)
            {
                dist = d;
                i = jj;
            } 
        }

        Vector3 a = gen.dots[i - 1];
        a = gen.transform.TransformPoint(a);
        Vector3 b = gen.dots[i % (gen.dots.Count)];
        b = gen.transform.TransformPoint(b);
        Vector3 m = Vector3.Lerp(a, b, 0.5f);

        Vector3 r = a;
        bool isMid = false;
        if (Vector3.Distance(mousePos, r) > Vector3.Distance(mousePos, b))
            r = b;
        if (Vector3.Distance(mousePos, r) > Vector3.Distance(mousePos, m))
        {
            isMid = true;
            Handles.DrawLine(mousePos,m);
            r = m;
        }

        return new FindMidResult(isMid, r, i-1, i);
    }
    
    public static float SegPointDist(Vector2 p, Vector2 a, Vector2 b) {
        var rx = p.x - a.x;
        var ry = p.y - a.y;
        var dx = b.x - a.x;
        var dy = b.y - a.y;
        var segLen = Mathf.Sqrt(dx * dx + dy * dy); // длина
        var rotAngle = -Mathf.Atan2(dy, dx);        // направление
        var angleCos = Mathf.Cos(rotAngle);
        var angleSin = Mathf.Sin(rotAngle);
        var lx = angleCos * rx - angleSin * ry;
        var ly = angleSin * rx + angleCos * ry;
        var segNearestX = Mathf.Max(0, Mathf.Min(lx, segLen));
        var localDiffX = lx - segNearestX;
        var localDiffY = ly;
        return localDiffX * localDiffX + localDiffY * localDiffY;
    }

    public struct FindMidResult
    {
        public bool createMid;
        public int ai, bi;
        public Vector3 point;
        public FindMidResult(bool _createMid, Vector3 _point, int _ai, int _bi)
        {
            createMid = _createMid;
            point = _point;
            ai = _ai;
            bi = _bi;
        }
    }
}
