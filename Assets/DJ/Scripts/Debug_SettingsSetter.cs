using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_SettingsSetter : MonoBehaviour
{
    public LevelSettings settings;

    private void Awake()
    {
        GlobalSettings.levelSettings = settings;
    }
}
