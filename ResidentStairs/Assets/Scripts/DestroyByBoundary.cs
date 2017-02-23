﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
            Destroy(other.gameObject);
    }
}
