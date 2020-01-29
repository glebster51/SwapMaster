using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public static bool handleInput { get; private set; }

    [SerializeField, Range(0, 10)] private float dragPercent;
    private Vector2 fp, lp;
    private float dragSqrDistance;

    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);

        dragSqrDistance = Screen.height * dragPercent / 100;
        dragSqrDistance *= dragSqrDistance;
    }

    private void Update()
    {
        if (handleInput)
        {
            if (Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    lp = touch.position;

                    if (Mathf.Abs(lp.x - fp.x) > dragSqrDistance || Mathf.Abs(lp.y - fp.y) > dragSqrDistance)
                    {
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {
                            if ((lp.x > fp.x))
                                GameController.GetInput(ArrowDirection.Right);
                            else
                                GameController.GetInput(ArrowDirection.Left);
                        }
                        else
                        {
                            if (lp.y > fp.y)
                                GameController.GetInput(ArrowDirection.Up);
                            else
                                GameController.GetInput(ArrowDirection.Down);
                        }
                    }
                }
            }
        }
#if UNITY_EDITOR
        if (handleInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                fp = Input.mousePosition;
                lp = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                lp = Input.mousePosition;
                Vector2 move = lp - fp;
                if (move.sqrMagnitude >= dragSqrDistance)
                {
                    if (move.x * move.x > move.y * move.y)
                    {
                        if (move.x > 0)
                            GameController.GetInput(ArrowDirection.Right);
                        else
                            GameController.GetInput(ArrowDirection.Left);
                    }
                    else
                    {
                        if (move.y > 0)
                            GameController.GetInput(ArrowDirection.Up);
                        else
                            GameController.GetInput(ArrowDirection.Down);
                    }
                }
            }
        }
#endif
    }

    public static void EnableInput(bool enabled)
    {
        handleInput = enabled;
    }
}
