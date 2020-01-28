using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Level Settings Asset")]
public class LevelSettingsAsset : SerializedScriptableObject
{
    public float duration;

    //vragi
    public GameObject prefab;
    public Vector2 time;
}
