using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
	public GameObject Enemy;
	public Vector3 SpawnRange;
	public int EnemyCount;
	public float TimeBetweenEnemySpawn;
	public float TimeBeforeFirstWave;
	public float TimeBetweenWaves;
	public bool SpawnEnabled;

	public BonusManager Bonuses;

	public enum WaveType
	{
		SINUS,
		ROWS,
		PLUS,
		ARC
	}

	void Start()
	{
		Bonuses.Notify();
		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(TimeBeforeFirstWave);

		while (true)
		{
			if(SpawnEnabled)
			{
				for (int i = 0; i < EnemyCount; i++)
				{
					if(SpawnEnabled)
					{
						float r = Random.Range(0.0f, 1.0f) * 4.0f;
						Vector2 p = Vector2.zero;

						if(r <= 1.0f)
						{
							p = ParametricCurves.Sinus((float)i / (float)EnemyCount);
						}
						else if(r <= 2.0f)
						{

						}
						else if(r <= 3.0f)
						{

						}
						else if(r <= 4.0f)
						{

						}

						Vector3 spawnPosition = new Vector3(SpawnRange.x, 3.5f * p.y, SpawnRange.z);

						GameObject enemy = Instantiate(Enemy);
						enemy.transform.position = spawnPosition;

						if(Bonuses.HasToPopABonus())
						{
							enemy.GetComponent<EnemyScript>().BonusCarried = Bonuses.GetNextBonus();
						}

						yield return new WaitForSeconds(TimeBetweenEnemySpawn);
					}
				}
			}

			yield return new WaitForSeconds(TimeBetweenWaves);
		}
	}
}
