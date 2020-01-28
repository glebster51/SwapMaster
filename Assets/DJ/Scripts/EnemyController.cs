using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyController : SerializedMonoBehaviour
{
    public static EnemyController instance { get; private set; }

    [SerializeField] public Vector2 spawnPoint { get; private set; }
    [SerializeField] public float deathY { get; private set; }

    //debug
    public float cd;
    [ListDrawerSettings(AlwaysAddDefaultValue = true)]
    public GameObject[] enemies;
    public bool pause;
    public int points;

    private List<Monster> monsters;
    private List<Monster> patternMonsters;

    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);

        monsters = new List<Monster>();
        patternMonsters = new List<Monster>();
        StartCoroutine(Spawn());
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

    //beta
    private IEnumerator Spawn()
    {
        while (!pause)
        {
            yield return new WaitForSeconds(cd);
            Vector3 pos = new Vector3(Random.Range(-spawnPoint.x, spawnPoint.x), spawnPoint.y);
            GameObject prefab = enemies[Random.Range(0, enemies.Length)];
            monsters.Add(Instantiate(prefab, pos, Quaternion.identity).GetComponent<Monster>());
        }
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
