using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {
	public GameObject Enemy;
    public GameObject Boss;
	public Vector3 SpawnRange;
	public int EnemyCount;
	public float TimeBetweenEnemySpawn;
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

				Debug.Log(waveType.ToString());

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

        yield return new WaitForSeconds(TimeBeforeBoss);
        
        
        Boss.SetActive(true);
        Instantiate(Boss, new Vector3(0.0f, 0.0f, 40.0f), Quaternion.Euler(0.0f,180.0f,90.0f));
	}
}
