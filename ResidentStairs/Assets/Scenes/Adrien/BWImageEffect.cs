using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BWImageEffect : MonoBehaviour {

    [Range (0, 1)]
    public float intensity;
    private Material material;
    public Color col;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/BWEffect"));
    }

    void Update()
    {
        col.r = (Mathf.Sin(0.3f * Time.fixedTime + 0) * 127 + 128)/256;
        col.g = (Mathf.Sin(0.3f * Time.fixedTime + 2) * 127 + 128)/256;
        col.b = (Mathf.Sin(0.3f * Time.fixedTime + 4) * 127 + 128)/256;
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_bwBlend", intensity);
        material.SetColor("_Color", col);
        Graphics.Blit(source, destination, material);
    }
}