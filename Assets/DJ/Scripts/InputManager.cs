using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) EnemyController.GetInput(ArrowDirection.Left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) EnemyController.GetInput(ArrowDirection.Right);
        if (Input.GetKeyDown(KeyCode.UpArrow)) EnemyController.GetInput(ArrowDirection.Up);
        if (Input.GetKeyDown(KeyCode.DownArrow)) EnemyController.GetInput(ArrowDirection.Down);
    }
}
