using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ArrowSkin : MonoBehaviour
{
    public SkinRef skinRight;
    public SkinRef skinUp;
    public SkinRef skinLeft;
    public SkinRef skinDown;

    [System.Serializable]
    public class SkinRef
    {
        public Vector3 localOffset;
        public Sprite front;
        public Sprite back;
        public Color color;
    }

    public SpriteRenderer front, back;
    public SpriteRenderer glow;

    public SimpleAI.DirectionsSwap direction;
    SimpleAI.DirectionsSwap ldirection;

    public void SetSkin(SimpleAI.DirectionsSwap dir)
    {
        ldirection = direction = dir;
        float a = 0f;
        Color c= new Color();
        SkinRef d = new SkinRef();
        switch (direction)
        {
            case SimpleAI.DirectionsSwap.Right:
                d = skinRight;
                break;
            case SimpleAI.DirectionsSwap.Up:
                d = skinUp;
                break;
            case SimpleAI.DirectionsSwap.Left:
                d = skinLeft;
                break;
            case SimpleAI.DirectionsSwap.Down:
                d = skinDown;
                break;
        }
        front.sprite = d.front;
        back.sprite = d.back;
        glow.transform.localPosition = d.localOffset;
        a = glow.color.a;
        c = new Color(d.color.r, d.color.g, d.color.b, a);
        glow.color = c;
    }

#if UNITY_EDITOR
    private void LateUpdate()
    {
        if (ldirection != direction)
            SetSkin(direction);
    }
#endif
}
