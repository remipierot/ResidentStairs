using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour {

    public Transform objectShaking;
    public Vector3 startPos;
    public float shake = 0;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
     
    void Start()
    {
        objectShaking = gameObject.GetComponent<Transform>();
        startPos = objectShaking.localPosition;
    }

    void Update()
    {
        if (shake > 0)
        {
            objectShaking.localPosition = new Vector3(objectShaking.localPosition.x, Random.insideUnitSphere.y * shakeAmount, Random.insideUnitSphere.z * shakeAmount);
            shake -= Time.deltaTime * decreaseFactor;

        }
        else {
            shake = 0.0f;
            objectShaking.localPosition = startPos;
        }
    }

}
