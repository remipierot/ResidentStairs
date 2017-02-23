using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour {

	public float TimeBetweenPotentialBonus;
	public float SatelliteProbability;
	public float MultiplicatorProbability;
	public float ShieldProbability;
	public float NukeProbability;

	public GameObject Satellite;
	public GameObject Multiplicator;
	public GameObject Shield;
	public GameObject Nuke;

	private GameObject NextBonus;
	private float LastSpawnTime = 0;

	public void Notify()
	{
		LastSpawnTime = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		if (LastSpawnTime > 0 && NextBonus == null)
		{
			float elapsedTime = Time.realtimeSinceStartup - LastSpawnTime;

			if(elapsedTime >= TimeBetweenPotentialBonus)
			{
				float r = Random.Range(0.0f, 1.0f);
				float firstFloor = SatelliteProbability;
				float secondFloor = firstFloor + MultiplicatorProbability;
				float thirdFloor = secondFloor + ShieldProbability;
				float fourthFloor = thirdFloor + NukeProbability;

				if(r < firstFloor)
				{
					NextBonus = Satellite;
				}
				else if (r < secondFloor)
				{
					NextBonus = Multiplicator;
				}
				else if(r < thirdFloor)
				{
					NextBonus = Shield;
				}
				else if(r < fourthFloor)
				{
					NextBonus = Nuke;
				}
			}
		}
	}

	public bool HasToPopABonus()
	{
		return NextBonus != null;
	}

	public GameObject GetNextBonus()
	{
		GameObject Bonus = NextBonus;

		if(HasToPopABonus())
		{
			LastSpawnTime = Time.realtimeSinceStartup;
			NextBonus = null;
		}

		return Bonus;
	}
}
