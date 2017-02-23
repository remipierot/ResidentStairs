using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeartBehaviour : MonoBehaviour {

    public GameObject bossParent;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shot") || other.CompareTag("Player"))
        {
            if (other.CompareTag("Shot"))
            {
                Destroy(other.gameObject);
            }

            bossParent.GetComponent<BossBehaviour>().takeHit();
        }
    }
}
