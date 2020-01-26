using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HorizontalGrider : MonoBehaviour
{
    public float step;
    private float w;
#if UNITY_EDITOR
    private void LateUpdate()
    {
        w = (transform.childCount - 1) * step;
        for (int i = 0; i < transform.childCount; i++)
        {
            var ch = transform.GetChild(i);
            
            ch.localPosition = Vector2.right * step * i - Vector2.right * w * 0.5f;
        }
    }
#endif

}
