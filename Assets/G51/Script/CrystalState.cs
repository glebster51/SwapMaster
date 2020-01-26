using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CrystalState : MonoBehaviour
{
    public List<SpriteRenderer> sprites;
    [Range(0f,1f)]
    public float value;
    private float lastValue;

    private void LateUpdate()
    {
        if (lastValue != value)
            SetValue(value);
    }

    public void SetValue(float newValue)
    {
        lastValue = value = newValue;
        for (int i = 0; i < sprites.Count; i++)
        {
            var s = sprites[i];
            if (s)
            {
                Color c = s.color;
                s.color = Color.Lerp(new Color(c.r, c.g, c.b, 0f), new Color(c.r, c.g, c.b, 1f), value);
            }
            else
                sprites.Remove(s);
        }
    }
}
