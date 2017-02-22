using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float tiltFactor;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawnDroite;
    [SerializeField] private Transform shotSpawnCentre;
    [SerializeField] private Transform shotSpawnGauche;
    [SerializeField] private Transform shotSpawnSat1;
    [SerializeField] private Transform shotSpawnSat2;

    [SerializeField] private float numberOfShots;
    [SerializeField] private float numberOfSatelittes;

    public Boundary boundary;
    [SerializeField] private  MeshRenderer cannonDroite_renderer;
    [SerializeField] private  MeshRenderer cannonCentre_renderer;
    [SerializeField] private  MeshRenderer cannonGauche_renderer;
    [SerializeField] private  MeshRenderer Sat1_renderer;
    [SerializeField] private  MeshRenderer Sat2_renderer;


    private float nextFire = 0.0f;
    [SerializeField] private float fireRate;

    // Use this for initialization
    void Start () {

    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0.0f, moveHorizontal);

        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3
        (
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x *(-tiltFactor)); 
            
        if(numberOfShots == 1)
        {
            cannonDroite_renderer.enabled = false;
            cannonCentre_renderer.enabled = true;
            cannonGauche_renderer.enabled = false;
        }
        else if (numberOfShots == 2)
        {
            cannonDroite_renderer.enabled = true;
            cannonCentre_renderer.enabled = false;
            cannonGauche_renderer.enabled = true;
        }
        else if (numberOfShots == 3)
        {
            cannonDroite_renderer.enabled = true;
            cannonCentre_renderer.enabled = true;
            cannonGauche_renderer.enabled = true;
        }

        if(numberOfSatelittes == 0)
        {
            Sat1_renderer.enabled = false;
            Sat2_renderer.enabled = false;
        }
        else if(numberOfSatelittes == 1)
        {
            Sat1_renderer.enabled = true;
            Sat2_renderer.enabled = false;
        }
        else if(numberOfSatelittes == 2)
        {
            Sat1_renderer.enabled = true;
            Sat2_renderer.enabled = true;       
        }

    }

    private void Update()
    {        
       // bool fire1 = Input.GetButton("Fire1"); 
       // bool fire2 = Input.GetButton("Fire2");

        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (numberOfShots == 1)
            {
                GameObject clone = Instantiate(shot, shotSpawnCentre.position, Quaternion.identity) as GameObject;
            }
            else if (numberOfShots == 2)
            {
                GameObject clone = Instantiate(shot, shotSpawnDroite.position, Quaternion.identity) as GameObject;
                GameObject clone2 = Instantiate(shot, shotSpawnGauche.position, Quaternion.identity) as GameObject;
            }
            else if (numberOfShots == 3)
            {
                GameObject clone = Instantiate(shot, shotSpawnDroite.position, Quaternion.identity) as GameObject;
                GameObject clone2 = Instantiate(shot, shotSpawnCentre.position, Quaternion.identity) as GameObject;
                GameObject clone3 = Instantiate(shot, shotSpawnGauche.position, Quaternion.identity) as GameObject;
            }

            if(numberOfSatelittes == 1)
            {
                GameObject clone = Instantiate(shot, shotSpawnSat1.position, Quaternion.identity) as GameObject;
            }
            else if(numberOfSatelittes == 2)
            {
                GameObject clone = Instantiate(shot, shotSpawnSat1.position, Quaternion.identity) as GameObject;
                GameObject clone2 = Instantiate(shot, shotSpawnSat2.position, Quaternion.identity) as GameObject;
            }
        }
    }

}
