using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {

    public int maxLife = 100;
    public int life = 100;

    public Transform m_transform;
    public GameObject m_hitParticleSystem;

    public Vector3 boundUp;
    public Vector3 boundDown;
    public float speed;
    bool goingUp = true;

	// Use this for initialization
	void Start () {
        m_transform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
		
        if(goingUp)
        {
            m_transform.position = Vector3.MoveTowards(m_transform.position, boundUp, speed * Time.deltaTime);
            if (m_transform.position.y >= boundUp.y) goingUp = false;
        }
        else
        {
            m_transform.position = Vector3.MoveTowards(m_transform.position, boundDown, speed * Time.deltaTime);
            if (m_transform.position.y <= boundDown.y) goingUp = true;
        }

	}

    public void takeHit(int hit)
    {
        life -= hit;
        m_hitParticleSystem.GetComponent<BossHitParticle>().hit = true;
        if (life == 0) Die();
    }

    void Die()
    {
        // DIE DIE DIE
    }
}
