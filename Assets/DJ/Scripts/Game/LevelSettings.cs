using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Level Settings")]
public class LevelSettings : ScriptableObject
{
    public float delayBeforeWaves;
    public List<Wave> waves;
}

[System.Serializable]
public class Wave
{
    public Vector2 delayAfterWave;
    public Vector2 waveDuration;
    public Vector2 waveTick;

    public WaveEnemy[] enemies;
}

[System.Serializable]
public class WaveEnemy
{
    public GameObject enemyPrefab;
    public float weight;
    [HideInInspector] public float realWeight;
}
