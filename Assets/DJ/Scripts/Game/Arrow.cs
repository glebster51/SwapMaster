using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Arrow : MonoBehaviour
{
    [SerializeField] protected ArrowsGraphicsAsset graphicsAsset;
    [SerializeField, ListDrawerSettings(AlwaysAddDefaultValue = true)] protected List<SpriteRenderer> changeAlphaSprites;
    
    public SpriteRenderer frontSprite;
    public SpriteRenderer backSprite;
    public SpriteRenderer glowSprite;

    [PropertyRange(0f, 1f)]
    public float alpha;
    public ArrowDirection direction;

    public void AnimatePressed()
    {
        StartCoroutine(ProgressAnimator(0.1f));
    }

    public void SetValue(float val)
    {
        if (changeAlphaSprites == null)
            return;

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

    public void SetDirection(ArrowDirection dir)
    {
        if (!graphicsAsset)
            return;

        if (dir == ArrowDirection.Random)
            dir = (ArrowDirection)Random.Range(0, (int)ArrowDirection.Random);

        direction = dir;

        ArrowGraphicsSettings settings = graphicsAsset.settings[dir];

        frontSprite.sprite = settings.frontSprite;
        backSprite.sprite = settings.backSprite;

        glowSprite.transform.localPosition = settings.localOffset;
        Color col = settings.color;
        col.a = glowSprite.color.a;
        glowSprite.color = col;
    }


    protected IEnumerator ProgressAnimator(float animationTime)
    {
        float t = 0f;
        while (t <= animationTime)
        {
            SetValue(1 - t / animationTime);
            t += Time.deltaTime;
            yield return null;
        }
        SetValue(0f);
    }

#if UNITY_EDITOR
    protected float lastAlpha;
    protected ArrowDirection lastDirection;
    private void OnValidate()
    {
        if (lastAlpha != alpha)
        {
            lastAlpha = alpha;
            SetValue(alpha);
        }
        if (lastDirection != direction)
        {
            lastDirection = direction;
            SetDirection(direction);
        }
    }
#endif
}
