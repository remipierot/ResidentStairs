﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniTorusBehaviour : MonoBehaviour {

    Transform m_transform;
    public Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed = 8.0f;
    bool moving = false;
	
    void Start()
    {
        m_transform = GetComponent<Transform>();
        Destroy(gameObject, 5.0f);
    }

	// Update is called once per frame
	void Update () {
        if (moving)
        {
            m_transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir.normalized;
        moving = true;
    }

}
