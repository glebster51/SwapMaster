using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Level Settings")]
public class LevelSettings : SerializedScriptableObject
{
    [SerializeField] public float delayBeforeWaves { get; private set; }
    [SerializeField] public List<Wave> waves { get; private set; }
}

[System.Serializable]
public sealed class Wave
{
    public Vector2 delayAfterWave;
    public Vector2 waveDuration;
    public Vector2 waveTick;

    public WaveEnemy[] enemies;
}

[System.Serializable]
public sealed class WaveEnemy
{
    public GameObject enemyPrefab;
    public float weight;
    [HideInInspector] public float realWeight;
}
