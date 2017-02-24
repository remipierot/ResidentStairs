using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour {


	private Rigidbody _Body;
	private BoxCollider _Collider0;
	private CapsuleCollider _Collider1;

	public float Speed = 1.0f;
	public DestroyedAnim KillScript;
	public Material DyingMaterial;
	public GameObject BonusCarried;
    
	public Material BlackMaterial;
	public Material WhiteMaterial;
	public GameObject Outline;
    public bool IsAlive = true;
	public BossHitParticle HitParticles;

    public ParticleSystem m_HitParticleSystem;

    private bool IsBlack = false;
    public float maxLife = 10;
    float life;

    public Animator anim;

	// Use this for initialization
	void Start ()
	{
		_Body = GetComponent<Rigidbody>();
		_Collider0 = GetComponent<BoxCollider>();
		_Collider1 = GetComponent<CapsuleCollider>();
        life = maxLife;
    }

	private void Update()
	{
		_Body.AddForce(-transform.up * Speed);
        
    }

	private void OnTriggerEnter(Collider other)
	{
		bool canBeKilled = (!FindObjectOfType<GameManagerBehavior>().switchColor && IsBlack) || (FindObjectOfType<GameManagerBehavior>().switchColor && !IsBlack);

		if ((other.CompareTag("Shot") || other.CompareTag("Player")))
		{
            if(other.CompareTag("Shot"))
            {
                Destroy(other.gameObject);
            }

            if(canBeKilled && transform.position.z < 28)
            {
                life--;
                m_HitParticleSystem.Emit(30);
                if (life <= 0)
                {
                    Die();
                }
            }
            else
            {
                anim.SetTrigger("animStart");
            }
        }
	}

	public void SetColor(bool black)
	{
		IsBlack = black;

		GetComponent<MeshRenderer>().material = (IsBlack) ? BlackMaterial : WhiteMaterial;
		Outline.GetComponent<MeshRenderer>().material = (IsBlack) ? WhiteMaterial : BlackMaterial;

		if(!FindObjectOfType<GameManagerBehavior>().switchColor)
		{
			SwapMaterial();
		}
	}

	public void SwapMaterial()
	{
		MeshRenderer render;
		MeshRenderer outlineRender;
		Material ship;
		Material outline;

		render = GetComponent<MeshRenderer>();
		outlineRender = Outline.GetComponent<MeshRenderer>();
		ship = render.material;
		outline = outlineRender.material;

		render.material = outline;
		outlineRender.material = ship;
	}

	public void Die()
    {
        IsAlive = false;
        Material dying = new Material(DyingMaterial);
		dying.SetFloat("_Displacement", 0);
		GetComponent<MeshRenderer>().material = dying;
        gameObject.tag = "Untagged"; // pour ne plus être considéré comme ennemi après la mort
		_Collider0.enabled = false;
		_Collider1.enabled = false;
		Outline.SetActive(false);
		HitParticles.hit = true;

		BonusManager bonusManager = FindObjectOfType<BonusManager>();
        if (bonusManager.HasToPopABonus())
		{
			BonusCarried = bonusManager.GetNextBonus();
			bonusManager.EmptyNextBonus();

            if(BonusCarried.GetComponent<BonusBehaviour>().bonusType == BonusBehaviour.BonusType.WEAPON)
                Instantiate(BonusCarried, transform.position, Quaternion.Euler(90.0f,0.0f,0.0f));
            else
			    Instantiate(BonusCarried, transform.position, Quaternion.identity);
		}
        
        KillScript.enabled = true;
		KillScript.SetMaterial(dying);

        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            if (gameObject.name == ("Enemy_PLAY"))
            {
                Invoke("LoadGame", 2.0f);
            }
            else if (gameObject.name == ("Enemy_QUIT"))
            {
                Application.Quit();
            }
        }
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("FinalScene", LoadSceneMode.Single);
    }
}
