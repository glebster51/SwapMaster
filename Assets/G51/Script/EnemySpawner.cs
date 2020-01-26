using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public float spawnWeight;
    public Vector2 timerRamge;
    public List<GameObject> enemys;

    private List<Transform> spawned = new List<Transform>();
    public float enemySpeed;
    public float EnemySpeedMod = 0f;


    private void Start()
    {
        StartCoroutine(SpawnLoop(0.5f));
    }

    public float deathY;
    public GameObject deadScreen;
    private void Update()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            Transform t = spawned[i];
            if (t)
            {
                if (t.position.y >= deathY)
                {
                    t.position += Vector3.down * enemySpeed * Time.deltaTime;
                }
                else
                {
                    if (t.GetComponent<SimpleAI>().alive)
                    {
                        StopAllCoroutines();
                        deadScreen.SetActive(true);
                        enabled = false;
                    }
                }
            }
            else
            {
                spawned.RemoveAt(i);
                i--;
            }
        }

        enemySpeed += EnemySpeedMod * Time.deltaTime;
    }

    IEnumerator SpawnLoop(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        Vector3 o = transform.position;
        Vector3 r = Vector3.right * spawnWeight * 0.5f;
        spawned.Add(Instantiate(enemys[Random.Range(0, enemys.Count)], transform.position + r * Random.Range(-1f, 1f), Quaternion.identity).transform);
        StartCoroutine(SpawnLoop(Random.Range(timerRamge.x, timerRamge.y) - EnemySpeedMod * enemySpeed * 3f));
    }


    private void OnDrawGizmos()
    {
        Vector3 o = transform.position;
        Vector3 r = Vector3.right * spawnWeight * 0.5f;
        Gizmos.DrawLine(o - r, o + r);
        
        o.y = deathY;
        r = Vector3.right * spawnWeight * 0.5f;
        Gizmos.DrawLine(o - r, o + r);
        
    }

    public Text counter;
    public float count;
    public void CounterAdd(int add)
    {
        count= count + add;
        counter.text = "" + count;
    }
}
