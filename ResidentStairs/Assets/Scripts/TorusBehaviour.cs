using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusBehaviour : MonoBehaviour {

	public GameObject miniTorus;
	Transform m_transform;
	public float speed;
	public float yDirection;
	public float nbMinTorus;
	private bool alive = true;

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

				if(alive)
					Die();
			}

			Destroy(this.gameObject);
		}
	}

	public void Die()
	{
		alive = false;
		Vector3 center = m_transform.position;
		Vector3 newPos = center;
		Vector2 parametricValues;
		GameObject go;
		int r = (int)(Random.Range(0.0f, 1.0f) * 3.99f);

		for (int i = 0; i < nbMinTorus; i++)
		{
			switch (r)
			{
				case 0:
					parametricValues = ParametricCurves.Circle((float)i / nbMinTorus);
					break;
				case 1:
					parametricValues = ParametricCurves.Plus((float)i / nbMinTorus);
					break;
				case 2:
					parametricValues = ParametricCurves.Square((float)i / nbMinTorus);
					break;
				default:
					parametricValues = ParametricCurves.Triangle((float)i / nbMinTorus);
					break;
			}

			newPos.z = center.z + 4.0f * parametricValues.x;
			newPos.y = center.y + 4.0f * parametricValues.y;
			go = Instantiate(miniTorus, newPos, m_transform.rotation);

			if((newPos - center).Equals(Vector3.zero))
				go.GetComponent<MiniTorusBehaviour>().setDirection(new Vector3(0.0f, yDirection, speed));
			else
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
