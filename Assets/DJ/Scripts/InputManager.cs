using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public static bool handleInput { get; private set; }

    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    private void Update()
    {
        if (handleInput)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) GameController.GetInput(ArrowDirection.Left);
            if (Input.GetKeyDown(KeyCode.RightArrow)) GameController.GetInput(ArrowDirection.Right);
            if (Input.GetKeyDown(KeyCode.UpArrow)) GameController.GetInput(ArrowDirection.Up);
            if (Input.GetKeyDown(KeyCode.DownArrow)) GameController.GetInput(ArrowDirection.Down);
        }
    }

    public static void EnableInput(bool enabled)
    {
        handleInput = enabled;
    }
}
