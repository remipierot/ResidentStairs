using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Halftone : MonoBehaviour
{
	private Material material;

	void Awake()
	{
		material = new Material(Shader.Find("Hidden/HalftoneShader")); ;
	}

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		float elapsedTime = material.GetFloat("_ElapsedTime") + Time.deltaTime;
		material.SetFloat("_ElapsedTime", elapsedTime);
		material.SetTexture("_MainTex", source);
		Graphics.Blit(source, destination, material);
	}
}
