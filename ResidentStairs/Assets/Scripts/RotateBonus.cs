using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBonus : MonoBehaviour
{

    Transform m_transform;
    public float rotationDirectionX = 0f;
    public float rotationDirectionY = 1f;
    public float rotationDirectionZ = 0f;


    // Use this for initialization
    void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.Rotate(new Vector3(rotationDirectionX * 90f * Time.deltaTime, rotationDirectionY * 90f * Time.deltaTime, rotationDirectionZ * 90f * Time.deltaTime));
    }
}