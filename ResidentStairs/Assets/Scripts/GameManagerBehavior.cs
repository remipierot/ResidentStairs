using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerBehavior : MonoBehaviour {

    public bool switchColor = false;
    [SerializeField] private float switchCooldown = 0.5f;
    private float nextSwitch = 0.0f;
    public GameObject background;
    public GameObject cam;

    public Material BlackControls;
    public Material WhiteControls;

    // Update is called once per frame
    void Update () {

        if(Input.GetButtonUp("Fire5"))
        {
            background.SetActive(!background.activeInHierarchy);
        }

        if (Input.GetButtonUp("Fire3") && Time.time > nextSwitch)
        {
            switchColor = !switchColor;
            nextSwitch = Time.time + switchCooldown;

            cam.GetComponent<BWImageEffect>().black = !cam.GetComponent<BWImageEffect>().black;

            EnemyScript[] enemies = FindObjectsOfType<EnemyScript>();
			foreach(EnemyScript e in enemies)
			{
                if(e.IsAlive)
                {
				    e.SwapMaterial();
                }
			}

            if (SceneManager.GetActiveScene().name == "FinalScene")
			{
				BossHeartBehaviour bossBehaviour = FindObjectOfType<BossHeartBehaviour>();

				if(bossBehaviour != null)
				{
					bossBehaviour.SwapMaterial();
				}
			}
            else if (SceneManager.GetActiveScene().name == "Controls")
            {
                var controlsRender = GameObject.Find("Controls").GetComponent<Renderer>();

                controlsRender.sharedMaterial = cam.GetComponent<BWImageEffect>().black ? BlackControls : WhiteControls;
            }
        }      
    }

    public void ResetGame()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the scene with the Single mode
        if(SceneManager.GetActiveScene().name == "TitleScreen")
        {
            SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("FinalScene", LoadSceneMode.Single);
        }
    }
}
