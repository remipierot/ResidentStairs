using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitParticle : MonoBehaviour {

    ParticleSystem m_partSystem;
    public bool hit = false;

	// Use this for initialization
	void Start () {
        m_partSystem = GetComponent<ParticleSystem>();

    }
	
	void Update()
    {
        if (hit)
        {
            m_partSystem.Emit(30);
            hit = false;
        }
    }
}
