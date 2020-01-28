using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Monster : SerializedMonoBehaviour
{
    [FoldoutGroup("Arrows"), SerializeField] public Transform arrowsContainer { get; protected set; }
    [FoldoutGroup("Arrows")] public float padding;

    [SerializeField] public float moveSpeed { get; protected set; }

    public int progress { get; protected set; }
    public ArrowDirection nextDirection { get; protected set; }

    protected Arrow[] arrows;
    protected ArrowDirection[] pattern;
    protected Animator anim;

    private void Awake()
    {
        progress = 0;
        anim = GetComponent<Animator>();
        arrows = arrowsContainer.GetComponentsInChildren<Arrow>(true);
        pattern = new ArrowDirection[arrows.Length];
        ResetProgress();
        RandomizePattern();
    }

    public bool AddProgress()
    {
        arrows[progress].AnimatePressed();
        anim.SetTrigger("GetDMG");

        progress++;
        if (progress >= pattern.Length)
            return true;

        nextDirection = pattern[progress];
        return false;
    }

    public void Die()
    {
        anim.SetBool("alive", false);
        Destroy(gameObject, 1f);
    }

    public void ResetProgress()
    {
        progress = 0;
        nextDirection = pattern[0];
        foreach (var arrow in arrows)
            arrow.SetValue(1);
    }

    protected void RandomizePattern()
    {
        if (arrows == null) return;
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetDirection(ArrowDirection.Random);
            pattern[i] = arrows[i].direction;
        }
        nextDirection = pattern[progress];
    }

    protected void AlignArrows()
    {
        int count = arrowsContainer.childCount;
        Vector2 half = Vector2.right * (count - 1) * padding * 0.5f;
        for (int i = 0; i < count; i++)
        {
            Transform arrow = arrowsContainer.GetChild(i);
            arrow.localPosition = Vector2.right * padding * i - half;
        }
    }

#if UNITY_EDITOR
    protected void OnValidate()
    {
        if (arrowsContainer) AlignArrows();
    }
#endif
}
