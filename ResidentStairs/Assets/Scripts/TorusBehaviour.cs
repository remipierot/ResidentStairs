using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusBehaviour : MonoBehaviour {

	public GameObject miniTorus;
	Transform m_transform;
	public float speed;
	public float yDirection;
	public float nbMinTorus;

	// Use this for initialization
	void Start () {
		m_transform = GetComponent<Transform>();
		Destroy(this.gameObject, 8.0f);
	}
	
	// Update is called once per frame
	void Update () {
		m_transform.position -= new Vector3(0.0f, yDirection * Time.deltaTime, speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.CompareTag("Shot") || other.CompareTag("Player")))
		{
			if (other.CompareTag("Shot"))
			{
				Destroy(other.gameObject);
				Die();
			}

			Destroy(this.gameObject);
		}
	}

	public void Die()
	{
		Vector3 center = m_transform.position;
		Vector3 newPos = center;
		Vector2 parametricValues;
		GameObject go;
		int r = (int)(Random.Range(0.0f, 1.0f) * 3.99f);

		for (int i = 0; i < nbMinTorus; i++)
		{
			/*
			switch (r)
			{
				case 0:
					parametricValues = ParametricCurves.Stars(1.6f, 32.0f * (float)i / nbMinTorus);
					break;
				case 1:
					parametricValues = ParametricCurves.Stars(1.5f, 12.0f * (float)i / nbMinTorus);
					break;
				case 2:
					parametricValues = ParametricCurves.Stars(0.5f, 12.0f * (float)i / nbMinTorus);
					parametricValues.x = (parametricValues.x + 1.0f) / 2.0f;
					parametricValues.y = parametricValues.y / 2.6f;
					break;
				default:
					parametricValues = ParametricCurves.Circle((float)i / nbMinTorus);
					break;
			}
			*/

			//For now, until better pattern found
			parametricValues = ParametricCurves.Circle((float)i / nbMinTorus);

			newPos.z = center.z + 4.0f * parametricValues.x;
			newPos.y = center.y + 4.0f * parametricValues.y;
			go = Instantiate(miniTorus, newPos, m_transform.rotation);
			go.GetComponent<MiniTorusBehaviour>().setDirection((newPos - center));
		}

		BonusManager bonusManager = FindObjectOfType<BonusManager>();
		if (bonusManager.HasToPopABonus())
		{
			GameObject bonus = bonusManager.GetNextBonus();
			bonusManager.EmptyNextBonus();

			if (bonus.GetComponent<BonusBehaviour>().bonusType == BonusBehaviour.BonusType.WEAPON)
				Instantiate(bonus, transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
			else
				Instantiate(bonus, transform.position, Quaternion.identity);
		}
	}
}
