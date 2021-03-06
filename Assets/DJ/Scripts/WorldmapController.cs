﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldmapController : MonoBehaviour
{
    [SerializeField] private Vector2 verticalBorders;
    [SerializeField] private Vector2 horizontalBorders;
    [SerializeField] private Transform levelsContainer;
    [SerializeField, Range(0, 20)] private float dragPercent;
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraSpeedDamp;

    private Camera mainCamera;
    private WorldmapLevel[] levels;

    private Vector3 fp, lp;
    private float dragSqrDistance;
    private bool isDragging;
    private Vector3 camStartPos;
    private float screenHeight;
    private Vector3 cameraVelocity;
    private float unitsPerPixel;

    private void Start()
    {
        screenHeight = Screen.height;
        mainCamera = Camera.main;
        dragSqrDistance = screenHeight * dragPercent / 100;
        dragSqrDistance *= dragSqrDistance;
        unitsPerPixel = (mainCamera.orthographicSize * 2) / screenHeight;
        levels = levelsContainer.GetComponentsInChildren<WorldmapLevel>(true);
        if (GlobalSettings.lastLevel != -1)
        {
            levels[GlobalSettings.lastLevel].SetScore(GlobalSettings.lastLevelScore);
        }
    }

    private void Update()
    {
        if (cameraVelocity.sqrMagnitude > 0.01)
        {
            Vector3 pos = mainCamera.transform.position + cameraVelocity * cameraMoveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, horizontalBorders.x, horizontalBorders.y);
            pos.y = Mathf.Clamp(pos.y, verticalBorders.x, verticalBorders.y);
            mainCamera.transform.position = pos;
            cameraVelocity = Vector3.Lerp(cameraVelocity, Vector3.zero, cameraSpeedDamp * Time.deltaTime);
        }

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
                Vector3 diff = fp - lp;
                diff.z = 0;
                diff *= unitsPerPixel;
                Vector3 pos = camStartPos + diff;
                pos.y = Mathf.Clamp(pos.y, verticalBorders.x + mainCamera.orthographicSize, verticalBorders.y - mainCamera.orthographicSize);
                pos.x = Mathf.Clamp(pos.x, horizontalBorders.x, horizontalBorders.y);
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
            cameraVelocity = lp - Input.mousePosition;
            cameraVelocity.z = 0;
            cameraVelocity *= unitsPerPixel;
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
        Gizmos.DrawLine(new Vector3(horizontalBorders.x, verticalBorders.x, 0), new Vector3(horizontalBorders.y, verticalBorders.x, 0));
        Gizmos.DrawLine(new Vector3(horizontalBorders.x, verticalBorders.x, 0), new Vector3(horizontalBorders.x, verticalBorders.y, 0));
        Gizmos.DrawLine(new Vector3(horizontalBorders.y, verticalBorders.y, 0), new Vector3(horizontalBorders.x, verticalBorders.y, 0));
        Gizmos.DrawLine(new Vector3(horizontalBorders.y, verticalBorders.y, 0), new Vector3(horizontalBorders.y, verticalBorders.x, 0));
    }
#endif
}
