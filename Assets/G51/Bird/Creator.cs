using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public GameObject go;
    public int count = 32;

    public int timer;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        for (int i = 0; i < timer; i++)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log(timer - i);
        }
        float steps = 7.5f;
        for (int i = 0; i < count; i++)
        {
            GameObject container = new GameObject("" + (i * count) + " - "  + (i * count + count));
            for (int j = 0; j < count; j++)
            {
                 var g = Instantiate(go, container.transform);
                g.transform.position = new Vector3(j, 0, i) * steps;
                g.name = "" + (count * i + j + 1);
            }
            yield return new WaitForSeconds(Time.deltaTime * 1.5f);
        }
         
    }
}
