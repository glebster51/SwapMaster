using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnWeight;
    public Vector2 timerRamge;
    public List<GameObject> enemys;

    private List<Transform> spawned = new List<Transform>();
    public float enemySpeed;


    private void Start()
    {
        StartCoroutine(SpawnLoop(Random.Range(timerRamge.x, timerRamge.y)));
    }

    private void Update()
    {
        for (int i = 0; i < spawned.Count; i++)
        {
            Transform t = spawned[i];
            if (t)
                 t.position += Vector3.down * enemySpeed * Time.deltaTime;
            else
            {
                spawned.RemoveAt(i);
                i--;
            }
        }
    }

    IEnumerator SpawnLoop(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        Vector3 o = transform.position;
        Vector3 r = Vector3.right * spawnWeight * 0.5f;
        spawned.Add(Instantiate(enemys[Random.Range(0, enemys.Count)], transform.position + r * Random.Range(-1f, 1f), Quaternion.identity).transform);
        StartCoroutine(SpawnLoop(Random.Range(timerRamge.x, timerRamge.y)));
    }


    private void OnDrawGizmos()
    {
        Vector3 o = transform.position;
        Vector3 r = Vector3.right * spawnWeight * 0.5f;
        Gizmos.DrawLine(o - r, o + r);
    }
}
