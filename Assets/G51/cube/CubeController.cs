using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public Transform cameraOrbit;
    public bool mouseClick;
    public float speed = 180f;

    private void LateUpdate()
    {

        mouseClick = Input.GetKey(KeyCode.Mouse0);
        if (mouseClick)
        {
            float y = Input.GetAxis("Mouse X") * Time.deltaTime * speed;
            cameraOrbit.localEulerAngles = cameraOrbit.localEulerAngles + new Vector3(0,y,0);
        }

    }
}
