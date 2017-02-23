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
		BLACK,
		WHITE,
		HALF,
		QUARTER
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
				int r = (int)(Random.Range(0.0f, 1.0f) * 3.99f);
				WaveType waveType;

				switch(r)
				{
					case 0: waveType = WaveType.BLACK;
						break;
					case 1: waveType = WaveType.WHITE;
						break;
					case 2: waveType = WaveType.HALF;
						break;
					default: waveType = WaveType.QUARTER;
						break;
				}

				for (int i = 0; i < EnemyCount; i++)
				{
					if(SpawnEnabled)
					{
						Vector2 p = Vector2.zero;
						p = ParametricCurves.Sinus((float)i / (float)EnemyCount);

						Vector3 spawnPosition = new Vector3(SpawnRange.x, 10.0f * p.y, SpawnRange.z);

						GameObject enemy = Instantiate(Enemy);
						EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
						enemy.transform.position = spawnPosition;

						bool enemyIsBlack =
							(waveType == WaveType.BLACK) ||
							(waveType == WaveType.HALF && i < EnemyCount / 2.0f) ||
							(waveType == WaveType.QUARTER && (i < EnemyCount / 4.0f || (i >= 2.0f * EnemyCount / 4.0f && i < 3.0f * EnemyCount / 4.0f)));

						enemyScript.SetColor(enemyIsBlack);

						if(Bonuses.HasToPopABonus())
						{
							enemyScript.BonusCarried = Bonuses.GetNextBonus();
						}

						yield return new WaitForSeconds(TimeBetweenEnemySpawn);
					}
				}
			}

			yield return new WaitForSeconds(TimeBetweenWaves);
		}
	}
}
