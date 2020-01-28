using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    public List<Wave> Waves;
    [System.Serializable]
    public class Wave
    {
        public Vector2 waveDelay;          // рэйндж задержки волны
        public Vector2 waveDuration;       // рэйндж продолжительности волны
        public Vector2 waveTick;           // рэйндж частоты спавна врагов
        public List<EnemyInWave> enemys;   // кто может заспавниться в волне
        [System.Serializable]
        public class EnemyInWave
        {
            public GameObject enemyPrefab;
            public float weight;      // вес - вероятность - соотношение - вероятности заспавниться в этой волне
        }
    }
}
