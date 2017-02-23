using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    bool alive = false;
    bool arriving = true;

    public ParticleSystem m_deathParticle;
	public BossHeartBehaviour m_heart;

    public GameObject torus;

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
                Invoke("ACTIVATINGMOTORS", 1.0f);
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

    void ACTIVATINGMOTORS()
    {
        alive = true;
        InvokeRepeating("shotTorus", 5.0f, 3.0f);
    }

    void shotTorus()
    {
        Vector3 curPos = m_transform.position;
        GameObject o = (GameObject)Instantiate(torus, curPos, Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f)));
        curPos.y += 5.0f;
        o.GetComponent<TorusBehaviour>().yDirection = 0.0f;
        o = (GameObject)Instantiate(torus, curPos, Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f)));
        curPos.y -= 10.0f;
        o.GetComponent<TorusBehaviour>().yDirection = -2.0f;
        o = (GameObject)Instantiate(torus, curPos, Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f)));
        o.GetComponent<TorusBehaviour>().yDirection = 2.0f;
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
		m_heart.StopSwapingColor();
        m_deathParticle.Emit(10000);
        m_meshContainer.SetActive(false);

        Invoke("ReloadTitleScene", 5.0f);
    }

    void ReloadTitleScene()
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}
