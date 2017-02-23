using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeartBehaviour : MonoBehaviour {

    public GameObject bossParent;
	public Material BlackMaterial;
	public Material WhiteMaterial;
	public float TimeBetweenColorSwitches;

	private bool IsBlack;
	private bool IsMaterialBlack;

	// Use this for initialization
	void Start ()
	{
		SetColor(true);
		StartCoroutine(SwapColor());
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shot") || other.CompareTag("Player"))
        {
            if (other.CompareTag("Shot"))
            {
                Destroy(other.gameObject);
            }

            bossParent.GetComponent<BossBehaviour>().takeHit(1);
        }
    }

	public void SwapMaterial()
	{
		MeshRenderer render;
		Material boss;

		render = GetComponent<MeshRenderer>();
		boss = render.material;

		render.material = (IsMaterialBlack) ? WhiteMaterial : BlackMaterial;
		IsMaterialBlack = !IsMaterialBlack;
	}

	public void SetColor(bool black)
	{
		IsBlack = black;
		IsMaterialBlack = black;

		GetComponent<MeshRenderer>().material = (IsBlack) ? BlackMaterial : WhiteMaterial;

		if (!FindObjectOfType<GameManagerBehavior>().switchColor)
		{
			SwapMaterial();
		}
	}

	private IEnumerator SwapColor()
	{
		while (true)
		{
			SetColor(!IsBlack);
			yield return new WaitForSeconds(TimeBetweenColorSwitches);
		}
	}
}
