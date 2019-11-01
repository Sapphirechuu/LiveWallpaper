using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraDepthNormSnow : MonoBehaviour
{
    public Texture2D snowTexture;
    public Color snowColor = Color.white;
    public float snowTextureScale = 0.1f;

    [Range(0, 1)]
    public float botThreshold = 0f;
    [Range(0, 1)]
    public float topThreshold = 1f;

    private Material _material;

    void OnEnabled()
    {
        _material = new Material(Shader.Find("Unlit/SnowOverlay"));
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _material.SetMatrix("_CamToWorld", GetComponent<Camera>().cameraToWorldMatrix);
        _material.SetColor("_SnowColor", snowColor);
        _material.SetFloat("_BottomThreshold", botThreshold);
        _material.SetFloat("_TopThreshold", topThreshold);
        _material.SetTexture("_SnowTex", snowTexture);
        _material.SetFloat("_SnowTexScale", snowTextureScale);

        Graphics.Blit(source, destination, _material);
    }
}
