using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {

    public int maxLife = 100;
    public int life = 100;

    public Transform m_transform;
    public GameObject m_hitParticleSystem;
    public GameObject m_meshContainer;

    public Vector3 midPos;
    public Vector3 boundUp;
    public Vector3 boundDown;
    public float speed;
    bool goingUp = true;
    bool alive = true;
    bool arriving = true;

    public ParticleSystem m_deathParticle;

	// Use this for initialization
	void Start () {
        m_transform = GetComponent<Transform>();
        arriving = true;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(arriving)
        {
            m_transform.position = Vector3.MoveTowards(m_transform.position, midPos, speed * Time.deltaTime);
            if (m_transform.position.z <= midPos.z)
            {
                arriving = false;
                alive = true;
            }
        }

        if(alive)
        {
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

    }

    public void takeHit(int hit)
    {
        if (alive)
        {
            life -= hit;
            m_hitParticleSystem.GetComponent<BossHitParticle>().hit = true;
            if (life <= 0) Die();
        }
    }

    void Die()
    {
        alive = false;
        m_deathParticle.Emit(10000);
        m_meshContainer.SetActive(false);
    }
}
