using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using System.Threading.Tasks;

public class Monster : MonoBehaviour
{
    [FoldoutGroup("Arrows"), SerializeField] private Transform arrowsContainer;
    [FoldoutGroup("Arrows")] public float padding;

    public float moveSpeed;
    public UnityEvent onGetDamage;
    public UnityEvent onDeath;

    public int progress { get; protected set; }
    public ArrowDirection nextDirection { get; protected set; }

    protected Arrow[] arrows;
    protected ArrowDirection[] pattern;
    protected Animator anim;

    private void Awake()
    {
        progress = 0;
        onDeath.AddListener(DeathAnimationCallback);
        anim = GetComponent<Animator>();
        arrows = arrowsContainer.GetComponentsInChildren<Arrow>(true);
        pattern = new ArrowDirection[arrows.Length];
        ResetProgress();
        RandomizePattern();
    }

    public bool AddProgress()
    {
        //Пересмотреть
        arrows[progress].AnimatePressed();
        if (onGetDamage != null)
            onGetDamage.Invoke();
        //anim.SetTrigger("GetDMG");

        progress++;
        if (progress >= pattern.Length)
            return true;

        nextDirection = pattern[progress];
        return false;
    }

    public void Die()
    {
        //Переписать
        if (onDeath != null)
            onDeath.Invoke();
        else
            DeathAnimationCallback();
    }

    private void DeathAnimationCallback()
    {
        Debug.Log("callback");
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
