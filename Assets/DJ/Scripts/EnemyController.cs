using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyController : SerializedMonoBehaviour
{
    public static EnemyController instance { get; private set; }

    [SerializeField] public Vector2 spawnPoint { get; private set; }
    [SerializeField] public float deathY { get; private set; }

    [SerializeField] public Transform enemyContainer { get; private set; }
    [SerializeField] public LevelSettings levelSettings { get; private set; }

    private List<Monster> monsters;
    private List<Monster> patternMonsters;

    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);

        monsters = new List<Monster>();
        patternMonsters = new List<Monster>();
        StartCoroutine(Spawner());
    }

    public static void GetInput(ArrowDirection dir)
    {
        List<Monster> remove = new List<Monster>();
        bool monsterDied = false;

        if (instance.patternMonsters.Count <= 0)
        {
            foreach (Monster item in instance.monsters)
                instance.patternMonsters.Add(item);
        }

        foreach (Monster monster in instance.patternMonsters)
        {
            if (monster.nextDirection == dir)
            {
                if (monster.AddProgress())
                {
                    instance.monsters.Remove(monster);
                    monsterDied = true;

                    remove.Add(monster);
                    monster.Die();
                }
            }
            else
            {
                monster.ResetProgress();
                remove.Add(monster);
            }
        }

        if (monsterDied)
        {
            instance.patternMonsters = new List<Monster>();
            foreach (Monster item in instance.monsters)
                instance.patternMonsters.Add(item);
        }
        else if (remove.Count > 0)
        {
            foreach (Monster item in remove)
                instance.patternMonsters.Remove(item);
        }
    }

    //хуйня
    private IEnumerator Spawner()
    {
        if (levelSettings == null) yield break;
        if (levelSettings.waves == null) yield break;
        if (levelSettings.waves.Count == 0) yield break;

        yield return new WaitForSeconds(levelSettings.delayBeforeWaves);

        float fullWeight = 0;
        foreach (Wave wave in levelSettings.waves)
            foreach (WaveEnemy waveEnemy in wave.enemies)
                fullWeight += waveEnemy.weight;

        fullWeight = 1 / fullWeight;

        foreach (Wave wave in levelSettings.waves)
            foreach (WaveEnemy waveEnemy in wave.enemies)
                waveEnemy.realWeight = waveEnemy.weight * fullWeight;

        int waveCount = levelSettings.waves.Count;
        for (int i = 0; i < waveCount; i++)
        {
            Wave wave = levelSettings.waves[i];
            float endTime = Time.time + Random.Range(wave.waveDuration.x, wave.waveDuration.y);
            while (Time.time < endTime)
            {
                float rand = Random.value;
                float sum = 0;
                foreach (WaveEnemy waveEnemy in wave.enemies)
                {
                    sum += waveEnemy.realWeight;
                    if (rand <= sum)
                    {
                        Vector3 pos = new Vector3(Random.Range(-spawnPoint.x, spawnPoint.x), spawnPoint.y);
                        monsters.Add(Instantiate(waveEnemy.enemyPrefab, pos, Quaternion.identity, enemyContainer).GetComponent<Monster>());
                        break;
                    }
                }
                yield return new WaitForSeconds(Random.Range(wave.waveTick.x, wave.waveTick.y));
            }
        }

        Debug.Log("End of level");
    }

    private void Update()
    {
        foreach (var item in monsters)
            item.transform.position += Vector3.down * item.moveSpeed * Time.deltaTime;

        var dead = monsters.FindAll(x => x.transform.position.y <= deathY);
        if (dead.Count > 0)
        {
            foreach (var item in dead)
            {
                monsters.Remove(item);
                if (patternMonsters.Contains(item))
                    patternMonsters.Remove(item);
                item.Die();
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(-spawnPoint.x, spawnPoint.y), spawnPoint);
        Gizmos.DrawLine(new Vector3(-spawnPoint.x, deathY), new Vector3(spawnPoint.x, deathY));
    }
#endif
}
