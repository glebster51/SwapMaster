using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable3D : MonoBehaviour
{
    public Animator animator;
    public bool isActive;
    public bool isSelected;
    public string keyEnter;
    public string keyExit;
    public string keyDown_Off;
    public string keyUp_Off;
    public string keyDown_On;
    public string keyUp_On;
    public UnityEvent OnClick;

    private void OnMouseEnter()
    {
        animator.SetTrigger(keyEnter);
        isSelected = true;
    }

    private void OnMouseExit()
    {
        animator.SetTrigger(keyExit);
        isSelected = false;
        
    }

    private void OnMouseDown()
    {
        if (isActive == false)
            animator.SetTrigger(keyDown_Off);
        else
            animator.SetTrigger(keyDown_On);
    }

    private void OnMouseUp()
    {
        if (isActive == false)
            animator.SetTrigger(keyUp_Off);
        else
            animator.SetTrigger(keyUp_On);
        isActive = !isActive;
        OnClick.Invoke();
    }
}
