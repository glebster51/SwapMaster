using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WorldmapLevel : MonoBehaviour
{
    [FoldoutGroup("Collision")] public Vector2 offset;
    [FoldoutGroup("Collision")] public float radius;

    public LevelSettings levelSettings;
    public LevelType levelType;
    public int healthOnLevel;

    public Vector2 colliderPosition { get; private set; }
    public int score { get; private set; }

    private LevelPinGraphic pin;

    public void SetScore(int value)
    {
        score = value;
        pin.SetStars(value);
    }

    private void Awake()
    {
        pin = GetComponentInChildren<LevelPinGraphic>();
        colliderPosition = transform.position;
        colliderPosition += offset;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        colliderPosition = transform.position;
        colliderPosition += offset;
        Gizmos.DrawWireSphere(colliderPosition, radius);
    }
#endif
}
