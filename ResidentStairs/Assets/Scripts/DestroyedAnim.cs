using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedAnim : MonoBehaviour {
	private Material m_material;
	public ParticleSystem m_particleSystem;
    
	public bool state = false;

	float disp = 0.0f;
	float dispMax = 20000.0f;

	public void SetMaterial(Material m)
	{
		m_material = m;
	}

	// Update is called once per frame
	void Update () {
		if (!state)
		{
			m_particleSystem.Emit(30);
		}

		disp += 3000.0f * Time.deltaTime;
		if (disp >= dispMax)
		{
			disp = 0.0f;
			state = false;
			Destroy(gameObject);
		}
		else
		{
			state = true;
		}

		m_material.SetFloat("_Displacement", disp);
	}
}
