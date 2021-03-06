﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSatelite : MonoBehaviour {

    Transform m_transform;
    public float rotationDirection = 1f;

	// Use this for initialization
	void Start () {
        m_transform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        m_transform.Rotate(new Vector3(0f, 0f, rotationDirection * 90f * Time.deltaTime));
	}
}
