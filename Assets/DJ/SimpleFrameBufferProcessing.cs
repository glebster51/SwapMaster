using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Camera))]
public class SimpleFrameBufferProcessing : MonoBehaviour 
{
    private Camera _camera;
    private RenderTexture _frameBuffer;

    public Vector2Int resolution = new Vector2Int(1280, 720);
    public enum Antialiasing { Off, x2, x4, x8 };
    public Antialiasing antialiasing = Antialiasing.x4; 

	void Start () 
	{
        _camera = GetComponent<Camera>();	
	}

    private void OnPreRender()
    {
        _camera.allowMSAA = false;
        _frameBuffer = RenderTexture.GetTemporary(resolution.x, resolution.y, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, Mathf.FloorToInt(Mathf.Pow(2, (int)antialiasing)));
        _camera.targetTexture = _frameBuffer;
    }

    private void OnPostRender()
    {
        _camera.targetTexture = null;
        Graphics.SetRenderTarget(null);
        Graphics.Blit(_frameBuffer, null as RenderTexture);
        RenderTexture.ReleaseTemporary(_frameBuffer);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SimpleFrameBufferProcessing)), CanEditMultipleObjects]
public class SimpleFrameBufferProcessingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SimpleFrameBufferProcessing targetSimpleFrameBufferProcessing = (SimpleFrameBufferProcessing)target;
		base.OnInspectorGUI();
	}
}
#endif
	