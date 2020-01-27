using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float cd;
    public GameObject enemy;
    public bool pause;
    public int points;
    public float spd;

    private List<Monster> monsters;

    private void Start()
    {
        monsters = new List<Monster>();
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        foreach (var item in monsters)
        {
            item.transform.position += Vector3.down * spd * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            List<Monster> m = monsters.FindAll(x => x.nextDirection == ArrowDirection.Left);
            if (m.Count > 0)
            {
                foreach (var item in m)
                {
                    if (item.AddProgress())
                    {
                        monsters.Remove(item);
                        item.Die();
                        points++;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            List<Monster> m = monsters.FindAll(x => x.nextDirection == ArrowDirection.Right);
            if (m.Count > 0)
            {
                foreach (var item in m)
                {
                    if (item.AddProgress())
                    {
                        monsters.Remove(item);
                        item.Die();
                        points++;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            List<Monster> m = monsters.FindAll(x => x.nextDirection == ArrowDirection.Up);
            if (m.Count > 0)
            {
                foreach (var item in m)
                {
                    if (item.AddProgress())
                    {
                        monsters.Remove(item);
                        item.Die();
                        points++;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            List<Monster> m = monsters.FindAll(x => x.nextDirection == ArrowDirection.Down);
            if (m.Count > 0)
            {
                foreach (var item in m)
                {
                    if (item.AddProgress())
                    {
                        monsters.Remove(item);
                        item.Die();
                        points++;
                    }
                }
            }
        }
    }

    private IEnumerator Spawn()
    {
        while (!pause)
        {
            yield return new WaitForSeconds(cd);
            monsters.Add(Instantiate(enemy, transform.position, Quaternion.identity).GetComponent<Monster>());
        }
    }
}
