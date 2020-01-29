using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance { get; private set; }

    public static bool handleInput { get; private set; }

    [SerializeField, Range(0, 10)] private float dragPercent;
    private Vector2 fp, lp;
    private float dragDistance;

    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);

        dragDistance = Screen.height * dragPercent / 100; //dragDistance is 10% height of the screen
    }

    private void Update()
    {
        if (handleInput)
        {
            if (Input.touchCount >= 1) // user is touching the screen with a single touch OR MORE
            {
                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                {
                    lp = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 10% of the screen height
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe
                                GameController.GetInput(ArrowDirection.Right);
                                Debug.Log("Right Swipe");
                            }
                            else
                            {   //Left swipe
                                GameController.GetInput(ArrowDirection.Left);
                                Debug.Log("Left Swipe");
                            }
                        }
                        else
                        {   //the vertical movement is greater than the horizontal movement
                            if (lp.y > fp.y)  //If the movement was up
                            {   //Up swipe
                                GameController.GetInput(ArrowDirection.Up);
                                Debug.Log("Up Swipe");
                            }
                            else
                            {   //Down swipe
                                GameController.GetInput(ArrowDirection.Down);
                                Debug.Log("Down Swipe");
                            }
                        }
                    }
                    else
                    {   //It's a tap as the drag distance is less than 10% of the screen height
                        Debug.Log("Tap");
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
                else if (Input.GetMouseButton(0))
                {
                    lp = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    lp = Input.mousePosition;

                    if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
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
                    else
                    {
                        Debug.Log("Tap");
                    }
                }
            }
#endif
        }
    }

    public static void EnableInput(bool enabled)
    {
        handleInput = enabled;
    }
}
