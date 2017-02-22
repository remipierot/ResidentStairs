using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	private Rigidbody _Body;

	public float Speed = 1.0f;

	// Use this for initialization
	void Start ()
	{
		_Body = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		_Body.AddForce(-transform.up * Speed);
	}

	void OnTriggerEnter(Collider other)
	{
		//Destroy(gameObject);
	}
}
