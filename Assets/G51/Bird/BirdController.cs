using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float angleSpeed;
    public float speed;
    public Transform cam;
    // Update is called once per frame
    void Update()
    {
        float dot = Vector3.Dot(Vector3.down, transform.forward);
        float fallMult = Mathf.Clamp01(dot) * 0.5f + 0.5f;
        Debug.Log(fallMult);
        transform.position += transform.forward * speed * Time.deltaTime * fallMult;
        
        Vector2 input = map(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        float mult = angleSpeed * Time.deltaTime;
        float spin = -Input.GetAxis("Spin") * mult;
        
        Quaternion y = Quaternion.Euler(0f, input.x * mult, 0f);
        Quaternion x = Quaternion.Euler(input.y * mult, 0f, 0f);
        Quaternion z = Quaternion.Euler(0f, 0f, spin);
        transform.rotation *= y * x * z;
        
        cam.position = transform.position;
        cam.rotation = Quaternion.Lerp(cam.rotation, transform.rotation * Quaternion.Euler(15f,0f,0f), Time.deltaTime * 5f);
    }
    
    Vector2 map(Vector2 input)
    {
        float x = input.x;
        float y = input.y;
        return new Vector2(
        x * Mathf.Sqrt(1 - y * y / 2), 
        y * Mathf.Sqrt(1 - x * x / 2));
    }
}
