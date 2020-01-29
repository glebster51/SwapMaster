using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WorldmapLevelCrystal : MonoBehaviour
{
    [SerializeField, ListDrawerSettings(AlwaysAddDefaultValue = true)] protected List<SpriteRenderer> changeAlphaSprites;
    [SerializeField, Range(0f,1f)] private float alpha;

    public void SetValue(float val)
    {
        alpha = val;

        for (int i = 0; i < changeAlphaSprites.Count; i++)
        {
            SpriteRenderer renderer = changeAlphaSprites[i];
            if (!renderer) continue;

            Color col = renderer.color;
            col.a = alpha;
            renderer.color = col;
        }
    }

#if UNITY_EDITOR
    private float lastAlpha;
    private void OnValidate()
    {
        if (alpha != lastAlpha)
        {
            lastAlpha = alpha;
            SetValue(alpha);
        }
    }
#endif
}
