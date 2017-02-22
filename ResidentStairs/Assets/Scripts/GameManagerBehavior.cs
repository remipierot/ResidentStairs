using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBehavior : MonoBehaviour {

    public bool switchColor = false;
    [SerializeField] private float switchCooldown = 0.5f;
    private float nextSwitch = 0.0f;

    public GameObject cam;

	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire2") && Time.time > nextSwitch)
        {
            switchColor = !switchColor;
            nextSwitch = Time.time + switchCooldown;

            cam.GetComponent<BWImageEffect>().black = !cam.GetComponent<BWImageEffect>().black;
        }      
    }
}
