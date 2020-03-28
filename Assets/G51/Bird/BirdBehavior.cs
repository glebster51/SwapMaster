using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BirdBehavior : MonoBehaviour
{
    public float speed;
    public Transform camtr;


    private Vector3 camPos;
    private void Start()
    {
        camPos = camtr.position - transform.position;
    }

    private void LateUpdate()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(Mathf.Sin(Time.time) * 45f, 0, 0);
        camtr.position = transform.position + camPos;
    }

    
}



