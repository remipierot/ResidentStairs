using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBehaviour : MonoBehaviour {

    Transform m_transform;
    public float speed;
    public float lifeTime;

    // Use this for initialization
    void Start()
    {
        m_transform = GetComponent<Transform>();
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.position -= new Vector3(0.0f, 0.0f, speed * Time.deltaTime);
    }

    public void catchBonus()
    {
        Destroy(gameObject);
    }
}
