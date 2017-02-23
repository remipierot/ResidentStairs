using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour {

    public Transform objectShaking;
    public Vector3 startPos;

    [Header("Shake Parameters")]
    public float shake = 0;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    [Header("Blink Parameters")]
    int blinkCpt = 0;
    public int maxBlink = 6;
    public float delayFirstBlink = 0.0f;
    public float delayBetweenBlink = 0.1f;
     
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
        InvokeRepeating("Blink", 0.0f, 0.1f);
        // Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().enabled = true;
    }

    void Blink()
    {
        Camera.main.GetComponent<BWImageEffect>().black = !Camera.main.GetComponent<BWImageEffect>().black;
        blinkCpt++;
        if(blinkCpt >= maxBlink)
        {
            blinkCpt = 0;
            CancelInvoke("Blink");
            // Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Vortex>().enabled = false;
        }
    }

}
