using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float yMin, yMax, zMin, zMax;

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
    [SerializeField] private bool barrierActive;

    public Boundary boundary;
    [SerializeField] private GameObject cannonDroite_renderer;
    [SerializeField] private GameObject cannonCentre_renderer;
    [SerializeField] private GameObject cannonGauche_renderer;
    [SerializeField] private GameObject sat1_renderer;
    [SerializeField] private GameObject sat2_renderer;
    [SerializeField] private GameObject barrier_renderer;

    private float nextFire = 0.0f;
    [SerializeField] private float fireRate;

    // Use this for initialization
    void Start () {
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bonus")
        {
            switch (other.name)
            {
                case "Weapon":
                    if (numberOfShots < 3) numberOfShots++;
                    break;
                case "Shield":
                    if (!barrierActive) barrierActive = true;
                    break;
                case "Satellite":
                    if (numberOfSatelittes < 3) numberOfSatelittes++;
                    break;
                default:
                    break;
            }
            other.gameObject.GetComponent<BonusBehaviour>().catchBonus();
        }
        else if(other.tag == "Enemy")
        {
            if (barrierActive) barrierActive = false;
            else Mort();
        }
    }

    void Mort()
    {
        Debug.Log("Mort");
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0.0f, moveVertical, moveHorizontal);

        rigidbody.velocity = movement * speed;

        rigidbody.position = new Vector3
        (
            0.0f,
            Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax),
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );
        
        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, 90+rigidbody.velocity.y *(-tiltFactor));


        /**********************************************
        **                                           **  
        **  ACTIVATION/DESACTIVATION DES GAMEOBJECT  **
        **                                           **  
        ***********************************************/
        if (numberOfShots == 1)
        {
            cannonDroite_renderer.SetActive(false);
            cannonCentre_renderer.SetActive(true);
            cannonGauche_renderer.SetActive(false);
        }
        else if (numberOfShots == 2)
        {
            cannonDroite_renderer.SetActive(true);
            cannonCentre_renderer.SetActive(false);
            cannonGauche_renderer.SetActive(true);
        }
        else if (numberOfShots == 3)
        {
            cannonDroite_renderer.SetActive(true);
            cannonCentre_renderer.SetActive(true);
            cannonGauche_renderer.SetActive(true);
        }

        if (numberOfSatelittes == 0)
        {
            sat1_renderer.SetActive(false);
            sat2_renderer.SetActive(false);
        }
        else if (numberOfSatelittes == 1)
        {
            sat1_renderer.SetActive(true);
            sat2_renderer.SetActive(false);
        }
        else if (numberOfSatelittes == 2)
        {
            sat1_renderer.SetActive(true);
            sat2_renderer.SetActive(true);
        }

        if (barrierActive) barrier_renderer.SetActive(true);
        else barrier_renderer.SetActive(false);

        
    }

    /**********************************************
    **                                           **  
    **              GESTION DES TIRS             **
    **                                           **  
    ***********************************************/

    private void Update()
    {        
       // bool fire1 = Input.GetButton("Fire1"); 
       // bool fire2 = Input.GetButton("Fire2");

        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (numberOfShots == 1)
            {
                GameObject clone = Instantiate(shot, shotSpawnCentre.position, Quaternion.Euler(0.0f,0.0f,90.0f)) as GameObject;
            }
            else if (numberOfShots == 2)
            {
                GameObject clone = Instantiate(shot, shotSpawnDroite.position, Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
                GameObject clone2 = Instantiate(shot, shotSpawnGauche.position, Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
            }
            else if (numberOfShots == 3)
            {
                GameObject clone = Instantiate(shot, shotSpawnDroite.position, Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
                GameObject clone2 = Instantiate(shot, shotSpawnCentre.position, Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
                GameObject clone3 = Instantiate(shot, shotSpawnGauche.position, Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
            }

            if(numberOfSatelittes == 1)
            {
                GameObject clone = Instantiate(shot, shotSpawnSat1.position, Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
            }
            else if(numberOfSatelittes == 2)
            {
                GameObject clone = Instantiate(shot, shotSpawnSat1.position, Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
                GameObject clone2 = Instantiate(shot, shotSpawnSat2.position, Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
            }
        }
    }

}
