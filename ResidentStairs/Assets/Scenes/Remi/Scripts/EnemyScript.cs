using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	private Rigidbody _Body;
	private BoxCollider _Collider0;
	private CapsuleCollider _Collider1;

	public float Speed = 1.0f;
	public DestroyedAnim KillScript;
	public Material DyingMaterial;
	public GameObject BonusCarried;
	public bool IsBlack = false;

	// Use this for initialization
	void Start ()
	{
		_Body = GetComponent<Rigidbody>();
		_Collider0 = GetComponent<BoxCollider>();
		_Collider1 = GetComponent<CapsuleCollider>();
	}

	private void Update()
	{
		_Body.AddForce(-transform.up * Speed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Shot") || other.CompareTag("Player"))
		{
			Die();
		}
	}

	private void Die()
	{
		Material dying = new Material(DyingMaterial);
		dying.SetFloat("_Displacement", 0);
		GetComponent<MeshRenderer>().material = dying;
		_Collider0.enabled = false;
		_Collider1.enabled = false;

		if(BonusCarried != null)
		{
			Instantiate(BonusCarried, transform.position, Quaternion.identity);
		}

		KillScript.enabled = true;
		KillScript.SetMaterial(dying);
	}
}
