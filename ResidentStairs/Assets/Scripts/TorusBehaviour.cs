using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusBehaviour : MonoBehaviour {

    public GameObject miniTorus;
    Transform m_transform;
    public float speed;
    public float yDirection;
    public float nbMinTorus;

    // Use this for initialization
    void Start () {
        m_transform = GetComponent<Transform>();
        Destroy(this.gameObject, 8.0f);
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
		Vector2 parametricValues;
		GameObject go;

		for (int i = 0; i < nbMinTorus; i++)
        {
			parametricValues = ParametricCurves.Circle((float)i / nbMinTorus);
            newPos.z = center.z + 4.0f * parametricValues.x;
            newPos.y = center.y + 4.0f * parametricValues.y;
            go = Instantiate(miniTorus, newPos, m_transform.rotation);
            go.GetComponent<MiniTorusBehaviour>().setDirection((newPos - center));

            // SPIRALE
        }

        Destroy(this.gameObject);
    }
}
