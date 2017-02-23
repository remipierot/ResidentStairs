using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
    public float yMin, yMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

    [SerializeField] private AudioSource shotSound;
    [SerializeField] private AudioSource bombSound;

    [SerializeField] private float speed;
    [SerializeField] private float tiltFactor;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawnDroite;
    [SerializeField] private Transform shotSpawnCentre;
    [SerializeField] private Transform shotSpawnGauche;
    [SerializeField] private Transform shotSpawnSat1;
    [SerializeField] private Transform shotSpawnSat2;

    [SerializeField] private Material myMat;


    [SerializeField] private int numberOfShots;
    [SerializeField] private int numberOfSatelittes;
    [SerializeField] private int numberOfBombs;
    [SerializeField] private bool barrierActive;

    public Boundary boundary;
    [SerializeField] private GameObject cannonDroite_renderer;
    [SerializeField] private GameObject cannonCentre_renderer;
    [SerializeField] private GameObject cannonGauche_renderer;
    [SerializeField] private GameObject sat1_renderer;
    [SerializeField] private GameObject sat2_renderer;
    [SerializeField] private GameObject barrier_renderer;
    [SerializeField] private GameObject bombHaut;
    [SerializeField] private GameObject bombCentre;
    [SerializeField] private GameObject bombBas;

    [SerializeField] private GameObject gameManager;


    private float nextFire = 0.0f;
    [SerializeField] private float fireRate;

    private float nextBomb = 0.0f;
    [SerializeField] private float bombRate;

    // Use this for initialization
    void Start () {
        myMat.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 1.0f));
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bonus"))
        {
            switch (other.GetComponent<BonusBehaviour>().bonusType)
            {
                case BonusBehaviour.BonusType.WEAPON:
                    if (numberOfShots < 3) numberOfShots++;
                    break;
                case BonusBehaviour.BonusType.SHIELD:
                    if (!barrierActive)
                    {
                        barrierActive = true;
                        myMat.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 1.0f));
                    }
                    break;
                case BonusBehaviour.BonusType.SAT:
                    if (numberOfSatelittes < 3)
                    {
                        numberOfSatelittes++;
                    }
                    if (numberOfSatelittes >= 3) numberOfSatelittes = 2;
                    break;
                case BonusBehaviour.BonusType.BOMB:
                    if (numberOfBombs < 3) numberOfBombs++;
                    break;
                default:
                    break;
            }
            other.gameObject.GetComponent<BonusBehaviour>().catchBonus();
        }
        else if(other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            if (barrierActive)
            {
                barrierActive = false;
                myMat.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f, 1.0f));
            }
            else Invoke("Mort", 3.0f);
        }
    }

    void Mort()
    {
        gameManager.GetComponent<GameManagerBehavior>().ResetGame();
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


        if (numberOfBombs == 0)
        {
            bombHaut.SetActive(false);
            bombCentre.SetActive(false);
            bombBas.SetActive(false);
        }
        else if (numberOfBombs == 1)
        {
            bombHaut.SetActive(false);
            bombCentre.SetActive(true);
            bombBas.SetActive(false);
        }
        else if (numberOfBombs == 2)
        {
            bombHaut.SetActive(true);
            bombCentre.SetActive(false);
            bombBas.SetActive(true);
        }
        else if (numberOfBombs == 3)
        {
            bombHaut.SetActive(true);
            bombCentre.SetActive(true);
            bombBas.SetActive(true);
        }

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

            shotSound.Play();

            if (numberOfShots == 1)
            {
                Instantiate(shot, shotSpawnCentre.position, Quaternion.Euler(0.0f,0.0f,90.0f));
            }
            else if (numberOfShots == 2)
            {
                Instantiate(shot, shotSpawnDroite.position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
                Instantiate(shot, shotSpawnGauche.position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
            }
            else if (numberOfShots == 3)
            {
                Instantiate(shot, shotSpawnDroite.position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
                Instantiate(shot, shotSpawnCentre.position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
                Instantiate(shot, shotSpawnGauche.position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
            }

            if(numberOfSatelittes == 1)
            {
                Instantiate(shot, shotSpawnSat1.position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
            }
            else if(numberOfSatelittes == 2)
            {
                Instantiate(shot, shotSpawnSat1.position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
               Instantiate(shot, shotSpawnSat2.position, Quaternion.Euler(0.0f, 0.0f, 90.0f));
            }
        }

        if (numberOfBombs > 0)
        {
            if (Input.GetButton("Fire2") && Time.time > nextBomb)
            {
                nextBomb = Time.time + bombRate;

                bombSound.Play();

                numberOfBombs--;

                Camera.main.GetComponent<Screenshake>().shake = 0.8f;

                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<EnemyScript>().Die();
                }
            }
        }
    }

}
