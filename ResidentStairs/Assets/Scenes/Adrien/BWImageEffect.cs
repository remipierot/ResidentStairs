using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BWImageEffect : MonoBehaviour {

    [Range (0, 1)]
    public float ramp;

    private Material material;

    public bool black = false;

    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/BWEffect"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_Ramp", ramp);

        if(black) material.SetFloat("_Invert", 0.0f);
        else material.SetFloat("_Invert", 1.0f);

        Graphics.Blit(source, destination, material);
    }
}