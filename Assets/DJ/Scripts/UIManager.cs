using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [SerializeField] private Transform deathScreen;

    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);

        deathScreen.gameObject.SetActive(false);
    }

    public static void ShowDeathScreen()
    {
        instance.deathScreen.gameObject.SetActive(true);
    }

    public static void SetHealth(int amount)
    {

    }
}
