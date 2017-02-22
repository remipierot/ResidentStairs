using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedAnim : MonoBehaviour {

    public Material m_material;
    public ParticleSystem m_particleSystem;
    
    public bool state = false;

    float disp = 0.0f;
    float dispMax = 0.3f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
            if (!state) m_particleSystem.Emit(30);
            disp += 0.1f * Time.fixedDeltaTime;
            if (disp >= dispMax)
            {
                disp = 0.0f;
                state = false;
            }
            else state = true;

            m_material.SetFloat("_Displacement", disp);


    }
}
