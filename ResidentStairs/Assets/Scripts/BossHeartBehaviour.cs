using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeartBehaviour : MonoBehaviour {

    public GameObject bossParent;
	public Material BlackMaterial;
	public Material WhiteMaterial;

	private bool IsBlack;

	// Use this for initialization
	void Start ()
	{
		IsBlack = true;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shot") || other.CompareTag("Player"))
        {
            if (other.CompareTag("Shot"))
            {
                Destroy(other.gameObject);
            }

            bossParent.GetComponent<BossBehaviour>().takeHit(1);
        }
    }

	public void SwapMaterial()
	{
		MeshRenderer render;
		Material boss;

		render = GetComponent<MeshRenderer>();
		boss = render.material;

		render.material = (IsBlack) ? WhiteMaterial : BlackMaterial;
	}
}
