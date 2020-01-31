using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class LevelPinGraphic : MonoBehaviour
{
    [Range(0,3)][SerializeField]
    private int stars;
    private int l_stars;
    [SerializeField]private List<GameObject> states;
    
    public void SetStars(int newStars)
    {
        l_stars = stars = Mathf.Clamp(newStars, 0, 3);
        for (int i = 0; i < states.Count; i++)
            states[i].SetActive(newStars == i);
    }
    
#if UNITY_EDITOR
    private void LateUpdate()
    {
        if (l_stars != stars)
            SetStars(stars);
    }
#endif
}
