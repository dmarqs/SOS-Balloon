using UnityEngine;
using System.Collections;

public class DificultyControl : MonoBehaviour
{
	[SerializeField]
	int nextDificulty;
	int dificultyVariation = 5;
	BalloonSpawnManager balloonSpawn;
	int dificultyControl = 0;
	public int count;
	GameManager gameManager;

	void Start ()
	{
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
		balloonSpawn = GetComponent<BalloonSpawnManager>();

		StartCoroutine(TimerToNewDificulty());
	}
	
	IEnumerator TimerToNewDificulty()
	{
		while (true)
		{
			if (!gameManager.isPaused)
			{
				for (int i = 0; i < nextDificulty; i++) yield return 0;

				ChangeDificulty();
			}
			yield return 0;
		}
	}

	void ChangeDificulty()
	{
		count++;
		if (dificultyControl == 0)
		{
			if (balloonSpawn.minNextFrameToSpawn > 10) balloonSpawn.minNextFrameToSpawn -= dificultyVariation-2;
		}
		else if (dificultyControl == 1)
		{
			if (balloonSpawn.maxNextFrameToSpawn > 9) balloonSpawn.maxNextFrameToSpawn -= dificultyVariation-3;
		}
		else if (dificultyControl == 2)
		{
			if (balloonSpawn.maxUpBalloonSpeed < 110) balloonSpawn.maxUpBalloonSpeed += dificultyVariation-1;
		}
		else if (dificultyControl == 3)
		{
			if (balloonSpawn.minUpBalloonSpeed < 100) balloonSpawn.minUpBalloonSpeed += dificultyVariation-2;
		}

		dificultyControl++;
		if (dificultyControl > 3) dificultyControl = 0;
	}
}