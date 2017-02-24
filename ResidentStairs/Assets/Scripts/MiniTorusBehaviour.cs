using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniTorusBehaviour : MonoBehaviour {

    Transform m_transform;
    public Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed = 8.0f;
    bool moving = false;
	private float internalVelocity = 0.0f;
	private Vector3 centerPoint = Vector3.zero;

    void Start()
    {
        m_transform = GetComponent<Transform>();
        Destroy(gameObject, 5.0f);
    }

	// Update is called once per frame
	void Update () {
        if (moving)
        {
			Vector3 newPos = m_transform.position + direction * speed * Time.deltaTime;
			Vector3 offset = (Quaternion.Euler(90.0f, 0, 0) * (newPos - m_transform.position)).normalized;
			newPos += offset * speed * Time.deltaTime;
			direction = (newPos - centerPoint).normalized;

			m_transform.position = newPos;
		}
    }

    public void setDirection(Vector3 dir)
    {
        direction = dir.normalized;
		centerPoint = transform.position - dir;
        moving = true;
    }

}
