﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusBehaviour : MonoBehaviour {

    public GameObject miniTorus;
    Transform m_transform;
    public float speed;
    public float yDirection;

	// Use this for initialization
	void Start () {
        m_transform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        m_transform.position -= new Vector3(0.0f, yDirection * Time.deltaTime, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Shot") || other.CompareTag("Player")))
        {
            if (other.CompareTag("Shot"))
            {
                Destroy(other.gameObject);
            }

            Die();
        }
    }

    public void Die()
    {
        Vector3 center = m_transform.position;
        Vector3 newPos = center;

        for (int i = 0; i < 12; i++)
        {
            newPos.z = center.z + 4.0f * Mathf.Cos(((float)i / 12.0f) * Mathf.PI * 2.0f);
            newPos.y = center.y + 4.0f * Mathf.Sin(((float)i / 12.0f) * Mathf.PI * 2.0f);
            GameObject go = (GameObject)Instantiate(miniTorus, newPos, m_transform.rotation);
            go.GetComponent<MiniTorusBehaviour>().setDirection((newPos - center));
        }

        Destroy(this.gameObject);
    }
}