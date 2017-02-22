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
    [SerializeField] GameObject shot;
    [SerializeField] Transform shotSpawnDroite;
    [SerializeField] Transform shotSpawnCentre;
    [SerializeField] Transform shotSpawnGauche;


    [SerializeField] private float numberOfShots;
    [SerializeField] private float numberOfSatelittes;

    public Boundary boundary;
    public MeshRenderer cannonDroite_renderer;
    public MeshRenderer cannonCentre_renderer;
    public MeshRenderer cannonGauche_renderer;

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
                GameObject clone = Instantiate(shot, shotSpawnCentre.position, shotSpawnCentre.rotation) as GameObject;
            }
            else if (numberOfShots == 2)
            {
                GameObject clone = Instantiate(shot, shotSpawnDroite.position, shotSpawnDroite.rotation) as GameObject;
                GameObject clone2 = Instantiate(shot, shotSpawnGauche.position, shotSpawnGauche.rotation) as GameObject;
            }
            else if (numberOfShots == 3)
            {
                GameObject clone = Instantiate(shot, shotSpawnDroite.position, shotSpawnDroite.rotation) as GameObject;
                GameObject clone2 = Instantiate(shot, shotSpawnCentre.position, shotSpawnCentre.rotation) as GameObject;
                GameObject clone3 = Instantiate(shot, shotSpawnGauche.position, shotSpawnGauche.rotation) as GameObject;
            }
        }
    }

}
