using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
	public GameObject Enemy;
    public GameObject Boss;
	public Vector3 SpawnRange;
	public int MaxEnemyPerWave;
	public int MinEnemyPerWave;
	public float TimeBeforeFirstWave;
	public float TimeBetweenWaves;
    public float TimeBeforeBoss;
    public bool SpawnEnabled;

	public BonusManager Bonuses;
    public int numWaves = 10;
    int waveCpt = 0;

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
        waveCpt = 0;
        Boss.gameObject.SetActive(false);
    }

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(TimeBeforeFirstWave);

		while (waveCpt < numWaves)
		{
            waveCpt++;

            if (SpawnEnabled)
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

				int enemyCount = (int)(Random.Range(0.0f, 1.0f) * (MaxEnemyPerWave - MinEnemyPerWave)) + MinEnemyPerWave;
				r = (int)(Random.Range(0.0f, 1.0f) * 3.99f);

				for (int i = 0; i < enemyCount; i++)
				{
					if(SpawnEnabled)
					{
						Vector2 p = Vector2.zero;

						switch (r)
						{
							case 0:
								p = ParametricCurves.Stars(1.5f, 12.0f * (float)i / (float)enemyCount);
								break;
							case 1:
								p = ParametricCurves.Sinus((float)i / (float)enemyCount);
								p.x = (p.x * 2.0f) - 1.0f;
								break;
							case 2:
								p = ParametricCurves.Stars(0.5f, 12.0f * (float)i / (float)enemyCount);
								p.x = (p.x + 1.0f) / 2.0f;
								p.y = p.y / 2.6f;
								break;
							default:
								p = ParametricCurves.PatternQueen(1, 2, 2, 1, 3, 3, 7.0f * (float)i / (float)enemyCount);
								p.x = (p.x + 0.5f) / 1.5f;
								p.y = p.y / 1.5f;
								break;
						}

						Vector3 spawnPosition = new Vector3(SpawnRange.x, 15.0f * p.y, SpawnRange.z + 15.0f * p.x);

						GameObject enemy = Instantiate(Enemy);
						EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
						enemy.transform.position = spawnPosition;

						bool enemyIsBlack =
							(waveType == WaveType.BLACK) ||
							(waveType == WaveType.HALF && i < enemyCount / 2.0f) ||
							(waveType == WaveType.QUARTER && (i < enemyCount / 4.0f || (i >= 2.0f * enemyCount / 4.0f && i < 3.0f * enemyCount / 4.0f)));

						enemyScript.SetColor(enemyIsBlack);
					}
				}
			}

			yield return new WaitForSeconds(TimeBetweenWaves);
		}

        yield return new WaitForSeconds(TimeBeforeBoss);
        
        
        Boss.SetActive(true);
        Instantiate(Boss, new Vector3(0.0f, 0.0f, 40.0f), Quaternion.Euler(0.0f,180.0f,90.0f));
	}
}
