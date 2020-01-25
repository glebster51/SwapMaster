﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    public List<DirectionsSwap> swaps;
    public List<CrystalState> crystals;
    private List<Coroutine> anims = new List<Coroutine>();
    public enum DirectionsSwap{Right, Up, Left, Down}
    public Transform arrow;
    
    private int progress = 0;
    private bool r, u, l, d;
    
    private void Start()
    {
        InitialEnemy();
    }

    void InitialEnemy()
    {
        for (int i = 0; i < swaps.Count; i++)
            swaps[i] = (DirectionsSwap)Random.Range(0f, 4f);
        foreach (var crystal in crystals)
            crystal.SetValue(0f);
        SetProgress(0);
    }

    void SetProgress(int newValue)
    {
        progress = newValue;
        if (progress < swaps.Count)
            arrow.right = SwapToVector(swaps[progress]); 
    }

    private void Update()
    {
        if (GetInputs())
        {
            if (r)
                TestDid(DirectionsSwap.Right);
            else if (u)
                TestDid(DirectionsSwap.Up);
            else if (l)
                TestDid(DirectionsSwap.Left);
            else if (d)
                TestDid(DirectionsSwap.Down);

            if (progress >= swaps.Count)
                Death();
        }
    }


    void Death()
    {
        Destroy(gameObject.gameObject, 1f);
        GetComponent<Animator>().SetBool("alive", false);
    }
    
    void TestDid(DirectionsSwap dir)
    {
        if (swaps[progress] == dir)
            GoodClick();
        else
            MissClick();
    }
   
    IEnumerator ProgressAnimator(CrystalState cs, float animationTime)
    {
        SetProgress(progress + 1);
        if (swaps.Count > progress)
            arrow.right = SwapToVector(swaps[progress]);
        float t = 0f;
        while (t <= animationTime)
        {
            cs.SetValue(t / animationTime);
            t += Time.deltaTime;
            yield return null;
        }
        cs.SetValue(1f);
    }

    void GoodClick()
    {
        anims.Add(StartCoroutine(ProgressAnimator(crystals[progress], 0.1f)));
        GetComponent<Animator>().SetTrigger("GetDMG");
    }
    void MissClick()
    {
        for (int i = 0; i < anims.Count; i++)
        {
            Coroutine anim = anims[i];
            anims.RemoveAt(i);
            if (anim != null)
                StopCoroutine(anim);
        }
        SetProgress(0);
        foreach (var crystal in crystals)
            crystal.SetValue(0f);
    }
    
    // ========================================================
    
    bool GetInputs()
    {
        r = Input.GetKeyDown(KeyCode.RightArrow);
        u = Input.GetKeyDown(KeyCode.UpArrow);
        l = Input.GetKeyDown(KeyCode.LeftArrow);
        d = Input.GetKeyDown(KeyCode.DownArrow);
        return r || u || l || d;
    }
    
    // ========================================================
    Vector2 SwapToVector(DirectionsSwap swap)
    {
        Vector2 answer = new Vector2();
        switch (swap)
        {
            case DirectionsSwap.Right:
                answer = Vector2.right;
                break;
            case DirectionsSwap.Up:
                answer = Vector2.up;
                break;
            case DirectionsSwap.Left:
                answer = Vector2.left;
                break;
            case DirectionsSwap.Down:
                answer = Vector2.down;
                break;
        }
        return answer;
    }

}