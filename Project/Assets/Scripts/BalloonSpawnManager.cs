using UnityEngine;
using System.Collections;

public class BalloonSpawnManager : MonoBehaviour
{
	public GameObject balloonModel;
	public GameObject[] spawnSectors;
	public int minNextFrameToSpawn;
	public int maxNextFrameToSpawn;
	public int minUpBalloonSpeed;
	public int maxUpBalloonSpeed;
	public int minSideBalloonSpeed;
	public int maxSideBalloonSpeed;
	GameManager gameManager;

	void Start ()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

		StartCoroutine(TimerToSpawnBalloon());
	}
	
	IEnumerator TimerToSpawnBalloon()
	{
		while (true)
		{
			if (!gameManager.isPaused)
			{
				int framesToNextSpawn = Random.Range(minNextFrameToSpawn, maxNextFrameToSpawn);

				for (int i = 0; i < framesToNextSpawn -1; i++) yield return 0;

				SpawnBalloon();
			}
			yield return 0;
		}
	}

	void SpawnBalloon()
	{
		Vector3 spawnPosition = spawnSectors[Random.Range(0, spawnSectors.Length)].transform.position;

		int sideDirection = ( Random.Range(0, 2) > 0 ) ? -1 : 1;

		balloonModel.GetComponent<BalloonControl>().upSpeed = Random.Range(minUpBalloonSpeed, maxUpBalloonSpeed) * 0.001f;
		balloonModel.GetComponent<BalloonControl>().sideSpeed = Random.Range(minSideBalloonSpeed, maxSideBalloonSpeed) * 0.001f * sideDirection;

		GameObject balloon = Instantiate(balloonModel, spawnPosition, transform.rotation) as GameObject;
		balloon.name = balloonModel.name;
	}
}