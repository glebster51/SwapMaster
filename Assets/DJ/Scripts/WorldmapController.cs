using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldmapController : MonoBehaviour
{
    [SerializeField] private Vector2 verticalBorders;
    [SerializeField] private Transform levelsContainer;
    [SerializeField, Range(0, 20)] private float dragPercent;
    [SerializeField] private float cameraMoveSpeed;

    private Camera mainCamera;
    private WorldmapLevel[] levels;

    private Vector3 fp, lp;
    private float dragSqrDistance;
    private bool isDragging;
    private Vector3 camStartPos;
    private float screenHeight;

    private void Start()
    {
        screenHeight = Screen.height;
        dragSqrDistance = screenHeight * dragPercent / 100;
        dragSqrDistance *= dragSqrDistance;
        mainCamera = Camera.main;
        levels = levelsContainer.GetComponentsInChildren<WorldmapLevel>(true);
        if (GlobalSettings.lastLevel != -1)
        {
            levels[GlobalSettings.lastLevel].SetScore(GlobalSettings.lastLevelScore);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fp = Input.mousePosition;
            lp = Input.mousePosition;
            camStartPos = mainCamera.transform.position;
        }
        
        if (Input.GetMouseButton(0))
        {
            lp = Input.mousePosition;
            if (!isDragging)
            {
                Vector2 move = lp - fp;
                if (move.sqrMagnitude >= dragSqrDistance)
                    isDragging = true;
            }
            else
            {
                //DRAG HERE
                float yDiff = lp.y - fp.y;
                Vector3 add = new Vector3(0, -yDiff / screenHeight * cameraMoveSpeed);
                Vector3 pos = camStartPos + add;
                pos.y = Mathf.Clamp(pos.y, verticalBorders.x + mainCamera.orthographicSize, verticalBorders.y - mainCamera.orthographicSize);
                mainCamera.transform.position = pos;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            if (!isDragging)
            {
                //TAP HERE
                Vector2 clickPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                for (int i = 0; i < levels.Length; i++)
                {
                    if ((levels[i].colliderPosition - clickPos).sqrMagnitude > levels[i].radius * levels[i].radius)
                        continue;
                    StartLevel(levels[i], i);
                    break;
                }
            }
            isDragging = false;
        }
    }

    public void StartLevel(WorldmapLevel level, int levelIndex)
    {
        GlobalSettings.startHealth = level.healthOnLevel;
        GlobalSettings.levelSettings = level.levelSettings;
        GlobalSettings.lastLevel = levelIndex;
        SceneManager.LoadScene((int)level.levelType);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(new Vector3(-20, verticalBorders.x, 0), new Vector3(20, verticalBorders.x, 0));
        Gizmos.DrawLine(new Vector3(-20, verticalBorders.y, 0), new Vector3(20, verticalBorders.y, 0));
    }
#endif
}
