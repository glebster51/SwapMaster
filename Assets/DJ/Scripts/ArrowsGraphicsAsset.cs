using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Arrows Graphics Asset")]
public class ArrowsGraphicsAsset : SerializedScriptableObject
{
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout, KeyLabel = "Direction", ValueLabel = "Settings")]
    public Dictionary<ArrowDirection, ArrowGraphicsSettings> settings;

#if UNITY_EDITOR
    private Dictionary<ArrowDirection, ArrowGraphicsSettings> resetSettings;
    [SerializeField, LabelText("Я понимаю чё ща будет")] private bool iUnderstandEverything;
    [Button("Создать полный пустой лист"), EnableIf("iUnderstandEverything")]
    private void CreateFullList()
    {
        resetSettings = new Dictionary<ArrowDirection, ArrowGraphicsSettings>();
        foreach (KeyValuePair<ArrowDirection, ArrowGraphicsSettings> item in settings)
            resetSettings.Add(item.Key, item.Value);

        iUnderstandEverything = false;

        settings = new Dictionary<ArrowDirection, ArrowGraphicsSettings>();
        int count = (int)ArrowDirection.Random;
        for (int i = 0; i < count; i++)
            settings.Add((ArrowDirection)i, new ArrowGraphicsSettings());
    }

    [SerializeField, LabelText("Я долбоёб")] private bool iAmDumb;
    [Button("Бля, я всё засрал! Есть сейв?"), EnableIf("iAmDumb")]
    private void ResetList()
    {
        iUnderstandEverything = false;
        settings = resetSettings;
    }
#endif
}