using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WorldmapLevel : MonoBehaviour
{
    [FoldoutGroup("Collision")] public Vector2 offset;
    [FoldoutGroup("Collision")] public float radius;

    public WorldmapLevelCrystal[] crystals;

    public LevelSettings levelSettings;
    public LevelType levelType;
    public int healthOnLevel;

    public Vector2 colliderPosition { get; private set; }
    public int score { get; private set; }
    
    public void SetScore(int value)
    {
        score = value;
        if (score > crystals.Length) score = crystals.Length;

        for (int i = 0; i < crystals.Length; i++)
            crystals[i].SetValue(i < score ? 1 : 0);
    }

    private void Awake()
    {
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
