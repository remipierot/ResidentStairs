using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour {

    public Transform objectShaking;
    public Vector3 startPos;
    public float shake = 0;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    int blinkCpt = 0;
     
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

    public void startShaking(float shakeVal)
    {
        CancelInvoke("Blink");
        shake = shakeVal;
        blinkCpt = 0;
        InvokeRepeating("Blink", 0.2f, 0.5f);
    }

    void Blink()
    {
        Camera.main.GetComponent<BWImageEffect>().black = !Camera.main.GetComponent<BWImageEffect>().black;
        blinkCpt++;
        if(blinkCpt >= 5)
        {
            blinkCpt = 0;
            CancelInvoke("Blink");
        }
    }

}
