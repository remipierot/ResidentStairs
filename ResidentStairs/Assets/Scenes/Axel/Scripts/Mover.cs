using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {


    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float speed;
    // Use this for initialization
    void Start () {
        rigidbody.velocity = transform.forward * speed;

        Destroy(gameObject, 2);
	}

    

}
