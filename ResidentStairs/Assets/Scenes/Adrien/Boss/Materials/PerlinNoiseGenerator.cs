using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseGenerator : MonoBehaviour {

    public Material m_material;

    public int pixWidth;
    public int pixHeight;
    public float xOrg;
    public float yOrg;
    public float scale = 1.0F;
    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;

    // Use this for initialization
    void Start () {
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
    }
	
	// Update is called once per frame
	void Update () {
        InvokeRepeating("changeNoise", 0.0f, 2.0f);
	}

    void changeNoise()
    {
        CalcNoise();
        m_material.SetTexture("_Noise", noiseTex);
    }

    void CalcNoise()
    {        
        float y = 0.0F;
        
        while (y < noiseTex.height)
        {
            float x = 0.0F;
            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(Time.time * xCoord, Time.time * yCoord);
                pix[(int)(y * noiseTex.width + x)] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }
}
