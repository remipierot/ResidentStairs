using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BWImageEffect : MonoBehaviour {

    [Range (0, 1)]
    public float ramp;
    [Range(0, 1)]
    public float invert;
    private Material material;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/BWEffect"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Ramp", ramp);
        material.SetFloat("_Invert", invert);
        Graphics.Blit(source, destination, material);
    }
}