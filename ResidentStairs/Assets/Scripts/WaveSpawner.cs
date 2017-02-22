using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
	public GameObject Enemy;
	public Vector3 SpawnRange;
	public int EnemyCount;
	public float TimeBetweenEnemySpawn;
	public float TimeBetweenFirstWave;
	public float TimeBetweenWaves;
	public bool SpawnEnabled;

	void Start()
	{
		StartCoroutine(SpawnWaves());
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(TimeBetweenFirstWave);

		while (true)
		{
			if(SpawnEnabled)
			{
				for (int i = 0; i < EnemyCount; i++)
				{
					if(SpawnEnabled)
					{
						Vector2 p = ParametricCurves.Sinus((float)i / (float)EnemyCount);
						Vector3 spawnPosition = new Vector3(SpawnRange.x, 2.5f * p.y, SpawnRange.z);

						GameObject enemy = Instantiate(Enemy);
						enemy.transform.position = spawnPosition;

						yield return new WaitForSeconds(TimeBetweenEnemySpawn);
					}
				}
			}

			yield return new WaitForSeconds(TimeBetweenWaves);
		}
	}
}
